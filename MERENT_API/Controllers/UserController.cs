﻿using Application.Interfaces;
using Application.ViewModels.AccountDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace MERENT_API.Controllers
{
    public class UserController: BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("accounts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllAccount()
        {
            var result = await _userService.GetAccountsAsync();
            return Ok(result);
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountByID(int Id)
        {
            var result = await _userService.GetAccountByIdAsync(Id);
            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUserAsync()
        {           
            var result = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (result == null || !int.TryParse(result, out int userId))
            {
                return BadRequest("Cannot find user ID.");
            }
           
            var user = await _userService.GetAccountByIdAsync(userId);
           
            if (user == null)
            {
                return NotFound("User not found.");
            }            
            return Ok(user);
        }

        [HttpGet("me2")]
        public async Task<IActionResult> GetCurrentUserAsync2()
        {
            var result = await _userService.GetCurrentUserAsync();
            if (result.Success || result.Data != null)
            {
                return Ok(result);
            }
            return Unauthorized(result);
        }

        [HttpGet("name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAccountByNameOrEmail(string name)
        {
            var result = await _userService.SearchAccountByNameAsync(name);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] AccountUpdateDTO updateDto)
        {
            var c = await _userService.UpdateAccountAsync(id, updateDto);
            if (!c.Success)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var c = await _userService.DeleteAccountAsync(id);
            if (!c.Success)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddAccountAsync([FromBody] AccountAddDTO addDto)
        {
            if (addDto == null)
            {
                return BadRequest();
            }
            var c = await _userService.AddAccountAsync(addDto);
            if (!c.Success)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }
    }
}
