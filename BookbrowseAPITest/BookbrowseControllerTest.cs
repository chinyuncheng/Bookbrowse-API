using Microsoft.Extensions.Logging;
using Moq;

using BookbrowseAPI.Controllers;
using BookbrowseAPI.Services;
using BookbrowseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookbrowseAPI.Test
{
    [TestClass]
    public class BookbrowseControllerTest
    {
        private readonly ILogger<BookbrowseController> _loggerMock;
        private readonly Mock<IBookDbService> _bookServiceMock;

        public BookbrowseControllerTest()
        {
            _loggerMock = Mock.Of<ILogger<BookbrowseController>>();
            _bookServiceMock = new Mock<IBookDbService>();
        }

        [TestMethod]
        public async Task Delete_RemovesExistingBook_Fail()
        {
            // Arrange
            var bookId = 1;
            _bookServiceMock.Setup(service => service.DeleteBookAsync(bookId)).ReturnsAsync(false);

            var controller = new BookbrowseController(_loggerMock, _bookServiceMock.Object);

            // Act
            var result = await controller.Delete(bookId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Delete_RemovesExistingBook_Success()
        {
            // Arrange
            var bookId = 1;
            _bookServiceMock.Setup(service => service.DeleteBookAsync(bookId)).ReturnsAsync(true);

            var controller = new BookbrowseController(_loggerMock, _bookServiceMock.Object);

            // Act
            var result = await controller.Delete(bookId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Get_ReturnsBook_Fail()
        {
            // Arrange
            int bookId = 1;
            Book? expectedBook = null;

            _bookServiceMock.Setup(service => service.GetBookAsync(bookId)).ReturnsAsync(expectedBook);

            var controller = new BookbrowseController(_loggerMock, _bookServiceMock.Object);

            // Act
            var result = await controller.Get(bookId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Get_ReturnsBook_Success()
        {
            // Arrange
            int bookId = 1;
            var expectedBook = new Book { Id = bookId, Title = "Book 1", Author = "Author 1", Year = 2024 };

            _bookServiceMock.Setup(service => service.GetBookAsync(bookId)).ReturnsAsync(expectedBook);

            var controller = new BookbrowseController(_loggerMock, _bookServiceMock.Object);

            // Act
            var result = await controller.Get(bookId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.AreEqual(expectedBook, okObjectResult.Value);
        }

        [TestMethod]
        public async Task Get_ReturnsBooks()
        {
            // Arrange
            var expectedBooks = new List<Book>
            {
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2024 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", Year = 2024 },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3", Year = 2024 },
            };

            _bookServiceMock.Setup(service => service.GetBooksAsync()).ReturnsAsync(expectedBooks);

            var controller = new BookbrowseController(_loggerMock, _bookServiceMock.Object);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            
            var okObjectResult = (OkObjectResult)result.Result;
            Assert.IsNotNull(okObjectResult.Value);
            var actualBooks = (IEnumerable<Book>)okObjectResult.Value;
            Assert.IsTrue(AssertHelper.IsEqual(expectedBooks, actualBooks));
        }

        [TestMethod]
        public async Task Post_CreatesNewBook_Fail()
        {
            // Arrange
            var newBook = new Book { Id = 1, Title = "New Book", Author = "New Author", Year = 2024 };
            Book? returnBook = null;
            _bookServiceMock.Setup(service => service.AddBookAsync(newBook)).ReturnsAsync(returnBook);

            var controller = new BookbrowseController(_loggerMock, _bookServiceMock.Object);

            // Act
            var result = await controller.Post(newBook);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Post_CreatesNewBook_Success()
        {
            // Arrange
            var newBook = new Book { Id = 1, Title = "New Book", Author = "New Author", Year = 2024 };
            _bookServiceMock.Setup(service => service.AddBookAsync(newBook)).ReturnsAsync(newBook);

            var controller = new BookbrowseController(_loggerMock, _bookServiceMock.Object);

            // Act
            var result = await controller.Post(newBook);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
            
            var createdAtActionResult = (CreatedAtActionResult)result.Result;
            Assert.AreEqual(newBook, createdAtActionResult.Value);
        }

        [TestMethod]
        public async Task Put_UpdatesExistingBook_Fail()
        {
            // Arrange
            int bookId = 1;
            var updatedBook = new Book { Id = bookId, Title = "Updated Book", Author = "Updated Author", Year = 2024 };
            _bookServiceMock.Setup(service => service.UpdateBookAsync(2, updatedBook)).ReturnsAsync(updatedBook);

            var controller = new BookbrowseController(_loggerMock, _bookServiceMock.Object);

            // Act
            var result = await controller.Put(bookId, updatedBook);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Put_UpdatesExistingBook_Success()
        {
            // Arrange
            int bookId = 1;
            var updatedBook = new Book { Id = bookId, Title = "Updated Book", Author = "Updated Author", Year = 2024 };
            _bookServiceMock.Setup(service => service.UpdateBookAsync(bookId, updatedBook)).ReturnsAsync(updatedBook);

            var controller = new BookbrowseController(_loggerMock, _bookServiceMock.Object);

            // Act
            var result = await controller.Put(bookId, updatedBook);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okObjectResult = (OkObjectResult)result.Result;
            Assert.AreEqual(updatedBook, okObjectResult.Value);
        }
    }
}
