using Microsoft.AspNetCore.Mvc;
using ShopApp.Users.Entity;
using ShopApp.Users.Repository.Interface;

namespace ShopApp.Users.Controller;

/// <summary>
/// Controller for managing users.
/// </summary>
//[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Retrieves a list of users.
    /// </summary>
    /// <param name="skip">Number of users to skip for pagination.</param>
    /// <param name="take">Number of users to take for pagination.</param>
    /// <returns>A list of users.</returns>
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUserss(int skip = 0, int take = 10)
    {
        var users = await _userRepository.GetAllUsersAsync();
        var paginatedUsers = users.Skip(skip).Take(take).ToList();
        return Ok(paginatedUsers);
    }

    /// <summary>
    /// Retrieves a user by its ID.
    /// </summary>
    /// <param name="id">The user ID.</param>
    /// <returns>The user.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="user">The updated user object.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }
        await _userRepository.UpdateUserAsync(user);
        return Ok(user);
    }

    /// <summary>
    /// Deletes a user by its ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        await _userRepository.DeleteUserAsync(id);
        return NoContent();
    }
}
