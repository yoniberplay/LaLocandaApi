using LaLocanda.Core.Application.Interfaces.Services;
using LaLocanda.Core.Application.ViewModels.Ingredient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LaLocandaApi.Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class IngredientController : BaseApiController
    {
        private readonly IIngredientService _ingService;

        public IngredientController(IIngredientService ingService)
        {
            _ingService = ingService;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(SaveIngredientViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(vm);
                }

                var ing = await _ingService.Add(vm);
                if (ing == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ing);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveIngredientViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, SaveIngredientViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(vm);
                }

                vm.Id = id;
                await _ingService.Update(vm, id);

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet("List")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngredientViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<IngredientViewModel> ings = await _ingService.GetAllViewModel();

                if (ings.Count == 0)
                    return NotFound();

                return Ok(ings);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveIngredientViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var ing = await _ingService.GetByIdViewModel(id);

                if (ing == null)
                    return NotFound();

                return Ok(ing);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
