using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApi.Core.DTO;
using TodoApi.Core.Inerface;
using TodoApi.Core.Model;

namespace TodoApi.Core.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/todos")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly Guid _userId;

        public TodosController(ITodoService todoService, IHttpContextAccessor httpContextAccessor)
        {
            _todoService = todoService;
            var userIdClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            _userId = Guid.Parse(userIdClaim!.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
    [FromQuery] TodoFilter filter,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
        {
            var todos = await _todoService.GetTodosAsync(_userId, filter, page, pageSize);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id, _userId);
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoCreateDto dto)
        {
            var todo = await _todoService.CreateTodoAsync(dto, _userId);
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TodoUpdateDto dto)
        {
            var todo = await _todoService.UpdateTodoAsync(id, dto, _userId);
            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _todoService.DeleteTodoAsync(id, _userId);
            return NoContent();
        }


    }
}
