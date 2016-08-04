using ASP.NET_Core_FileUploads.Models.Context;
using ASP.NET_Core_FileUploads.Models.Dto;
using ASP.NET_Core_FileUploads.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ASP.NET_Core_FileUploads.Test
{
    public class DocumentRepositoryTest
    {
        private static Mock<IFormFile> GetMockFormFileWithQuotedContentDisposition(string modelName, string filename)
        {
            var formFile = new Mock<IFormFile>();
            formFile.Setup(f => f.ContentDisposition)
                .Returns(string.Format("form-data; name='{0}'; filename=\"{1}\"", modelName, filename));

            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write("Text content for the test file");
            writer.Flush();

            ms.Position = 0;

            formFile.Setup(m => m.OpenReadStream()).Returns(ms);

            return formFile;
        }

        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }


        [Fact]
        public async Task Uploads_Stored_In_Documents_Folder()
        {
            var contextOptions = CreateNewContextOptions();

            var fileMock = GetMockFormFileWithQuotedContentDisposition("UploadTest", "UploadTest.txt");

            var mockEnvironment = new Mock<IHostingEnvironment>();
            mockEnvironment.Setup(m => m.WebRootPath).Returns(Path.GetTempPath());
            mockEnvironment.Setup(m => m.ContentRootPath).Returns(Path.GetTempPath());

            using (var context = new ApplicationDbContext(mockEnvironment.Object, contextOptions))
            {
                // Ensure the path exists.
                Directory.CreateDirectory(Path.GetTempPath() + "Uploads");
                
                var sut = new DocumentsRepository(context, mockEnvironment.Object);
                var file = fileMock.Object;

                var input = new DocumentInput()
                {
                    Name = "Upload Test",
                    File = fileMock.Object
                };
                //Act
                var result = await sut.Create(input);

                Assert.True(File.Exists(result.PathToFile));
                Assert.Equal(result.Name, input.Name);

                // Clean up.
                File.Delete(result.PathToFile);

            }
        }
    }
}

