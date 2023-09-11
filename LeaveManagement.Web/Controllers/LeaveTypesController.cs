using AutoMapper;
using LeaveManagement.Web.Contracts;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class LeaveTypesController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public LeaveTypesController(ILeaveTypeRepository leaveTypeRepository, IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
            this.leaveAllocationRepository = leaveAllocationRepository;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            //var leaveTypes = _mapper.Map<List<LeaveTypeVM>>(await _context.LeaveTypes.ToListAsync());
            var leaveTypes = await leaveTypeRepository.GetAllAsync();
            var leaevtypeVM = mapper.Map<List<LeaveTypeVM>>(leaveTypes);
            return View(leaevtypeVM);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var leaveType = await leaveTypeRepository.GetAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            var leaveTypeVM = mapper.Map<LeaveTypeVM>(leaveType);
            return View(leaveTypeVM);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeVM leaveTypeVM)
        {
            //if (ModelState.IsValid)
            //{
            //    var leaveType = _mapper.Map<LeaveType>(leaveTypeVM);
            //    _context.Add(leaveType);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(leaveTypeVM);

            // refactor
            if (ModelState.IsValid)
            {
                var leaveType = mapper.Map<LeaveType>(leaveTypeVM);
                await leaveTypeRepository.AddAsync(leaveType);

                return RedirectToAction(nameof(Index));
            }

            return View(leaveTypeVM);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //var leaveType = await _context.LeaveTypes.FindAsync(id);
            var leaveType = await leaveTypeRepository.GetAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            var leaveTypeVM = mapper.Map<LeaveTypeVM>(leaveType);
            return View(leaveTypeVM);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeVM leaveTypeVM)
        {
            if (id != leaveTypeVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var leaveType = mapper.Map<LeaveType>(leaveTypeVM);
                    //_context.Update(leaveType);
                    //await _context.SaveChangesAsync();
                    await leaveTypeRepository.UpdateAsync(leaveType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await leaveTypeRepository.Exist(leaveTypeVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeVM);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var leaveType = await _context.LeaveTypes.FindAsync(id);
            //_context.LeaveTypes.Remove(leaveType);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            // Refactor
            await leaveTypeRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocationLeave(int id)
        {
            await leaveAllocationRepository.LeaveAllocation(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
