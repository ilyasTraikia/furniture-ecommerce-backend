using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace test.Models
{
    public class UpdateService : IUpdateService { 

         private readonly ApplicationContext _context;

         public UpdateService(ApplicationContext context)
        {
          _context = context;
        }

    
        public async Task UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            { 
                    throw; 
            }

        
        }
    }
}
