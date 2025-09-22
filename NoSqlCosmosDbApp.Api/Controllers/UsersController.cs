using Microsoft.AspNetCore.Mvc;
using NoSqlCosmosDbApp.Domain.Interfaces;
using NoSqlCosmosDbApp.Domain.Models;

namespace NoSqlCosmosDbApp.Api.Controllers;

/// <summary>
/// API controller for managing users.
/// Provides endpoints to create, retrieve, update, and delete users.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService) : ControllerBase
{
    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetById(Guid id)
    {
        var user = await userService.GetByIdAsync(id);
        if (user is null)
            return NotFound();
        return Ok(user);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user object to create.</param>
    [HttpPost]
    public async Task<ActionResult<User>> Create(User user)
    {
        await userService.AddAsync(user);
        return Ok();
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The unique identifier of the user to update.</param>
    /// <param name="user">The updated user object.</param>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, User user)
    {
        await userService.UpdateAsync(user);
        return Ok();
    }

    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete.</param>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await userService.DeleteAsync(id);
        return NoContent();
    }
}