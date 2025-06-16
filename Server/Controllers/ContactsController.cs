using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhonebookApp.Data;
using PhonebookApp.Models;

namespace PhonebookApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ContactsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
    {
        return await _context.Contacts.ToListAsync();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Contact>>> SearchContacts(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return await _context.Contacts.ToListAsync();
        }

        return await _context.Contacts
            .Where(c => c.Name.Contains(query) || c.PhoneNumber.Contains(query))
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetContact(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);

        if (contact == null)
        {
            return NotFound();
        }

        return contact;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutContact(int id, Contact contact)
    {
        if (id != contact.Id)
        {
            return BadRequest();
        }

        _context.Entry(contact).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContactExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Contact>> PostContact(Contact contact)
    {
        // Check if phone number already exists
        if (_context.Contacts.Any(c => c.PhoneNumber == contact.PhoneNumber))
        {
            return BadRequest("Phone number already exists");
        }

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact == null)
        {
            return NotFound();
        }

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ContactExists(int id)
    {
        return _context.Contacts.Any(e => e.Id == id);
    }
}
