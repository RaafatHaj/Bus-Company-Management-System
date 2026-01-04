using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System.Text;
using Travel_Company_MVC.Services.Email;
using TravelCompany.Application.Services.Stations;
using TravelCompany.Domain.Entities;
using TravelCompany.Infrastructure.Persistence.Entities;

namespace Travel_Company_MVC.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IStationService _stationService;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IEmailService emailService, IStationService stationService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _emailService = emailService;
            _stationService = stationService;
        }

        [HttpGet]
        public async Task< IActionResult >Index()
        {
            //var user = await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);


            var users= await  _userManager.Users.Include(u=>u.Station).ToListAsync();

            //var model = _mapper.Map<IEnumerable<UserViewModel>>(users);

            var model = users.Select(u => new UserViewModel
            {
                Username=u.UserName!,
                Email=u.Email!,
                FullName=u.FullName,
                IsDeleted=u.IsDeleted,
                StationName=u.Station?.StationName,
                LastUpdatedOn=u.LastUpdatedOn

            });

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //var user = _userManager.FindByEmailAsync("rafatsh.m@gmail.com");

            //if(user is not null)
            //{

            //      await _emailService.SendConfimingEmailAsync(user.Result!);

            //}


           


            var satations =  await _stationService.GetAllStationsAsync();

            var model = new UserFormViewModel()
            {

                Roles = await _roleManager.Roles.Select(r =>
                              new SelectListItem()
                              {
                                  Text = r.Name,
                                  Value = r.Name

                              }).ToListAsync(),

                Stations = satations.Select(s => new SelectListItem
                {
                    Text = s.StationName,
                    Value = s.StationId.ToString()

                })
            };
                
            return View( model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(UserFormViewModel model)
        {




            if (!ModelState.IsValid)
                return BadRequest();

            var user = _mapper.Map<ApplicationUser>(model);

            user.CreatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            user.CreatedOn = DateTime.Now;

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, model.SelectedRoles);

                await _emailService.SendConfimingEmailAsync(user!);

                //var viewModel = _mapper.Map<UserViewModel>(user);

                return RedirectToAction("Index");
            }



            return BadRequest(string.Join(",",result.Errors.Select(e=>e.Description)));
        }


        //[HttpGet]
        //public async Task<IActionResult> SetPassword(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return RedirectToPage("/Index");
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound($"Unable to load user with ID '{userId}'.");
        //    }

        //    code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        //    var result = await _userManager.ConfirmEmailAsync(user, code);


        //}


        [HttpGet]

        public async Task<IActionResult> Edit(string userId)
        {

            var user =await _userManager.FindByIdAsync(userId);
           

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = _mapper.Map<UserFormViewModel>(user);
            model.SelectedRoles= roles;

            model.Roles = await _roleManager.Roles.Select(r =>
            new SelectListItem()
            {
                Text = r.Name,
                Value = r.Name
            }).ToListAsync();

            return PartialView("_Form", model);
        }


        [HttpPost]

        public async Task<IActionResult> Edit(UserFormViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(model.Id!);


            if (user == null)
                return NotFound();


            user =_mapper.Map(model,user);

            user.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            user.LastUpdatedOn = DateTime.Now;

            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded)
            {

                var currentRoles =await _userManager.GetRolesAsync(user);

                var isRolesUpdated=!currentRoles.SequenceEqual(model.SelectedRoles);

                if(isRolesUpdated)
                {

                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    await _userManager.AddToRolesAsync(user, model.SelectedRoles);

                }

                var viewModel = _mapper.Map<UserViewModel>(user);

                return PartialView("_UserRow", viewModel);

            }

            return BadRequest(string.Join(',', result.Errors.Select(e => e.Description)));
        }


        [HttpPost]

        public async Task<IActionResult> Delete(string userId)
        {

           var user =await _userManager.FindByIdAsync(userId);

            if(user== null)
                return BadRequest();

            user.IsDeleted = !user.IsDeleted;
            user.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            user.LastUpdatedOn = DateTime.Now;

            var result= await _userManager.UpdateAsync(user);

            if (user.IsDeleted)
                await _userManager.UpdateSecurityStampAsync(user); // sign out if the user deleted ...

      

            if(result.Succeeded)
            {
                return Ok(user.LastUpdatedOn.ToString());
            }


            return BadRequest(string.Join(',', result.Errors.Select(e => e.Description)));

        }

        [HttpPost]
        public async Task<IActionResult> UnLockUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();


            var isLocked = await _userManager.IsLockedOutAsync(user);

            if (isLocked)
                await _userManager.SetLockoutEndDateAsync(user, null);

            return Ok();

            //user.LockoutEnd = null;

            //var result = await _userManager.UpdateAsync(user);

            //if(result.Succeeded)
            //{
            //    return Ok();
            //}

            //return BadRequest();
        }


        public async Task<IActionResult> IsEmailAllowed(UserFormViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            return Json(user is null || user.Id.Equals(model.Id));
        }

        //public async Task<IActionResult> IsUserNameAllowed(UserFormViewModel model)
        //{
        //    //var user = await _userManager.FindByNameAsync(model.UserName);

        //    //return Json(user is null || user.Id.Equals(model.Id));
        //}


        private async Task<string> GetConfirmEmailUrl(ApplicationUser user)
        {

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var url= Url.Page(
                   "/Account/ConfirmEmail",
                   pageHandler: null,
                   values: new { area = "Identity", userId = user.Id, code },
                   protocol: Request.Scheme);

            return url!;
        }

    }
}
