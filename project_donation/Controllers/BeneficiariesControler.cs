using Microsoft.AspNetCore.Mvc;
using project_donation.Models.Beneficiarie;
using project_donation.context.beneficiaries;
namespace project_donation.Controllers
{
        public class BeneficiariesController : Controller
        {
            private readonly beneficiariesContex _Context;

            public BeneficiariesController(beneficiariesContex context)
            {
                _Context = context;
            }

            public IActionResult Index()
            {
                var beneficiaries = _Context.beneficiarie.ToList();
                return View(beneficiaries);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(Beneficiarie model)
            {
                _Context.beneficiarie.Add(model);
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }

            public IActionResult Update(int id)
            {
                var beneficiary = _Context.beneficiarie.Find(id);
                if (beneficiary == null)
                {
                    return NotFound();
                }
                return View(beneficiary);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Update(int id, Beneficiarie model)
            {
                if (id != model.id_Benefi)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    _Context.beneficiarie.Update(model);
                    _Context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(model);
            }

            public IActionResult Delete(int id)
            {
                var beneficiary = _Context.beneficiarie.Find(id);
                if (beneficiary == null)
                {
                    return NotFound();
                }
                return View(beneficiary);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirmed(int id)
            {
                var beneficiary = _Context.beneficiarie.Find(id);
                if (beneficiary == null)
                {
                    return NotFound();
                }
                _Context.beneficiarie.Remove(beneficiary);
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    
}
