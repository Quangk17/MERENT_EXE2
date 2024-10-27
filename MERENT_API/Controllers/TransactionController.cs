using Application.Interfaces;
using Application.ServiceRespones;
using Application.Services;
using Application.ViewModels.TransactionDTOs;
using Application.ViewModels.WalletDTOs;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MERENT_API.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllTransactions()
        {
            var result = await _transactionService.GetTransactionsAsync();
            return Ok(result);
        }

        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var result = await _transactionService.GetTransactionByIdAsync(id);

            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet("transactions-user")]
        public async Task<IActionResult> GetTransactionsBtUserId([FromQuery] WalletRequestTypeEnums walletTypeEnums)
        {
            try
            {
                var userIdString = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;


                if (userIdString == null || !int.TryParse(userIdString, out int userId))
                {
                    throw new Exception("User Id is invalid or you are not login");
                }
                var result = await _transactionService.GetTransactionsByUserId(userId, walletTypeEnums);
                return Ok(ServiceResponse<List<TransactionResponsesDTO>>.Succeed(result, "Get Transactions Of User with Id " + userId + " and type is " + walletTypeEnums.ToString() + " Successfully!"));
            }
            catch (Exception ex)
            {
                return BadRequest(ServiceResponse<object>.Fail(ex));
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionCreateDTO createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var result = await _transactionService.CreateTransactionAsync(createDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] TransactionUpdateDTO updateDto)
        {
            var result = await _transactionService.UpdateTransactionAsync(id, updateDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var result = await _transactionService.DeleteTransactionAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }

}
