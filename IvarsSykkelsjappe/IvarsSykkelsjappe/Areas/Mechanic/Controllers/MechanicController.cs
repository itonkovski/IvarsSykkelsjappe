using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Areas.Mechanic.Controllers
{
    using static MechanicConstants;

    [Area(AreaName)]
    [Authorize(Roles = MechanicRoleName)]
    public class MechanicController : Controller
    {

    }
}
