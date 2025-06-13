using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PhonebookApp.Controllers;
using PhonebookApp.Data;
using PhonebookApp.Models;
using Xunit;

namespace PhonebookApp.Controllers
{
    public class ContactsControllerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly ContactsController _controller;

        public ContactsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _controller = new ContactsController(_context);
        }

        [Fact]
        public async Task GetContacts_ReturnsAllContacts()
        {
            // Arrange
            _context.Contacts.AddRange(
                new Contact { Name = "Test1", PhoneNumber = "1111111111" },
                new Contact { Name = "Test2", PhoneNumber = "2222222222" }
            );
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetContacts();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Contact>>>(result);
            var returnValue = Assert.IsType<List<Contact>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetContact_ReturnsNotFound_ForInvalidId()
        {
            // Act
            var result = await _controller.GetContact(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostContact_ReturnsBadRequest_ForDuplicatePhoneNumber()
        {
            // Arrange
            var contact = new Contact { Name = "Test", PhoneNumber = "1234567890" };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            var duplicateContact = new Contact { Name = "Test2", PhoneNumber = "1234567890" };

            // Act
            var result = await _controller.PostContact(duplicateContact);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Phone number already exists", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteContact_ReturnsNotFound_ForInvalidId()
        {
            // Act
            var result = await _controller.DeleteContact(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}