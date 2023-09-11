using AutoMapper;
using LeaveManagement.Web.Constants;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using LeaveManagement.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace LeaveManagement.Web.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Employee> userManager;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;

        public LeaveAllocationRepository(ApplicationDbContext context,
            UserManager<Employee> userManager, ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper, IEmailSender emailSender) : base(context)
        {
            this.context = context;
            this.userManager = userManager;
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
            this.emailSender = emailSender;
        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
        {
            return await context.LeaveAllocations.AnyAsync(q => q.EmployeeId == employeeId 
                                                                && q.LeaveTypeId == leaveTypeId 
                                                                && q.Period == period);
        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int id)
        {
            var allocation = await context.LeaveAllocations
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (allocation == null)
            {
                return null;
            }

            var employee = await userManager.FindByIdAsync(allocation.EmployeeId);

            var model = mapper.Map<LeaveAllocationEditVM>(allocation);
            model.Employee = mapper.Map<EmployeeListVM>(employee);

            return model;
        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string employeeId)
        {
            var allocaitions = await context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Where(q => q.EmployeeId == employeeId)
                .ToListAsync();

            var employee = await userManager.FindByIdAsync(employeeId);
            
            var employeeAllocationModel = mapper.Map<EmployeeAllocationVM>(employee);
            employeeAllocationModel.LeaveAllocations = mapper.Map<List<LeaveAllocationVM>>(allocaitions);

            return employeeAllocationModel;
        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            var employees = await userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await leaveTypeRepository.GetAsync(leaveTypeId);
            var allocations = new List<LeaveAllocation>();

            foreach (var employee in employees)
            {
                if (await AllocationExists(employee.Id, leaveTypeId, period))
                    continue;

                var allocation = new LeaveAllocation()
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveTypeId,
                    Period = period,
                    NumberOfDays = leaveType.DefaultDays
                };
                allocations.Add(allocation);
            }
            await AddRangeAsync(allocations);
        }

        public async Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditVM model)
        {
            var leaveAllocation = await GetAsync(model.Id);
            if (leaveAllocation == null)
            {
                return false;
            }
            
            leaveAllocation.Period = model.Period;
            leaveAllocation.NumberOfDays = model.NumberOfDays;
            await UpdateAsync(leaveAllocation);

            var user = await userManager.FindByIdAsync(leaveAllocation.EmployeeId);
            await emailSender.SendEmailAsync(user.Email, $"Leave allocation update for {leaveAllocation.Period}",
                "Please review your leave allocation.");

            return true;
        }
    }
}
