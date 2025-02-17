using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class CompanyService : ICompanyService
    {
        private CodenationContext context;
        public CompanyService(CodenationContext context)
        {
            this.context = context;
        }

        public IList<Company> FindByAccelerationId(int accelerationId)
        {
            return context.Accelerations.Where(x => x.Id == accelerationId).
                SelectMany(x => x.Candidates).
                Select(x => x.Company).
                Distinct().
                ToList();
        }

        public Company FindById(int id)
        {
            return context.Companies.Find(id);
        }

        public IList<Company> FindByUserId(int userId)
        {
            return context.Users.Where(x => x.Id == userId).
                SelectMany(x => x.Candidates).
                Select(x => x.Company).
                Distinct().
                ToList();
        }

        public Company Save(Company company)
        {
            var status = company.Id == 0 ? EntityState.Added : EntityState.Modified;
            context.Entry(company).State = status;
            context.SaveChanges();
            return company;
        }
    }
}