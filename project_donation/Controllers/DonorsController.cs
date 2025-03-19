using Microsoft.AspNetCore.Mvc;
using project_donation.context.donor;
using project_donation.Models.Donor;
using project_donation.services.donor;

namespace project_donation.Controllers
{
    public class DonorsController : Controller
    {
       // private readonly DonationAdoService _DonationAdoService;
        private readonly donorContext _Context;
       // public DonorsController(DonationAdoService donationAdoService)
        public DonorsController(donorContext context) 
        {
            //_DonationAdoService = donationAdoService; 
            _Context = context;
        }

        public IActionResult Index()
        {
           // var donors = _DonationAdoService.Getall();
            var donors = _Context.donor.ToList();
            return View(donors); 
        }
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Donor model)
        {
            //_DonationAdoService.Add(model);
            _Context.donor.Add(model);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var donor = _Context.donor.Find(id);
            if (donor == null)
            {
                return NotFound();
            }
            return View(donor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Donor model)
        {
            if (id != model.id_donor)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _Context.donor.Update(model);
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var donor = _Context.donor.Find(id);
            if (donor == null)
            {
                return NotFound();
            }
            return View(donor); 
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var donor = _Context.donor.Find(id);
            if (donor == null)
            {
                return NotFound();
            }
            _Context.donor.Remove(donor);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}