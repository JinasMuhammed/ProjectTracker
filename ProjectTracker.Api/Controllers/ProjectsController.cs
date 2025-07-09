using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Application.Dtos;
using ProjectTracker.Application.Interfaces;

namespace ProjectTracker.Api.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _svc;
        public ProjectsController(IProjectService svc) => _svc = svc;

        public async Task<IActionResult> Index()
        {
            var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var list = await _svc.ListForUserAsync(ownerId);
            return View(list);
        }

        public IActionResult Create() => View(new CreateProjectDto());

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _svc.CreateAsync(dto, ownerId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var dto = await _svc.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, CreateProjectDto dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _svc.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _svc.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
