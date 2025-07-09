using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker.Application.Dtos;
using ProjectTracker.Application.Interfaces;

namespace ProjectTracker.Api.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITaskService _svc;
        public TasksController(ITaskService svc) => _svc = svc;

        // GET: /Tasks?projectId={projectId}
        public async Task<IActionResult> Index(Guid projectId)
        {
            ViewBag.ProjectId = projectId;
            var tasks = await _svc.ListByProjectAsync(projectId);
            return View(tasks);
        }

        // GET: /Tasks/Create?projectId={projectId}
        public IActionResult Create(Guid projectId)
        {
            ViewBag.ProjectId = projectId;
            return View(new CreateTaskDto());
        }

        // POST: /Tasks/Create?projectId={projectId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid projectId, CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProjectId = projectId;
                return View(dto);
            }

            await _svc.CreateAsync(projectId, dto);
            return RedirectToAction(nameof(Index), new { projectId });
        }

        // GET: /Tasks/Edit/{id}?projectId={projectId}
        public async Task<IActionResult> Edit(Guid id, Guid projectId)
        {
            var task = await _svc.GetByIdAsync(id);
            if (task == null) return NotFound();

            ViewBag.ProjectId = projectId;
            // Map TaskDto to CreateTaskDto for the form
            var editModel = new CreateTaskDto
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate
            };
            ViewData["TaskId"] = id;
            return View(editModel);
        }

        // POST: /Tasks/Edit/{id}?projectId={projectId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Guid projectId, CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProjectId = projectId;
                ViewData["TaskId"] = id;
                return View(dto);
            }

            await _svc.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index), new { projectId });
        }

        // GET: /Tasks/Delete/{id}?projectId={projectId}
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id, Guid projectId)
        {
            var task = await _svc.GetByIdAsync(id);
            if (task == null) return NotFound();

            ViewBag.ProjectId = projectId;
            return View(task);
        }

        // POST: /Tasks/Delete/{id}?projectId={projectId}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id, Guid projectId)
        {
            await _svc.DeleteAsync(id);
            return RedirectToAction(nameof(Index), new { projectId });
        }
    }
}
