using AutoMapper;
using Blog.Application.DTOs.PostDTOs;
using Blog.Application.IRepositories;
using Blog.Application.Services.PostServices;
using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Moq;

namespace Blog.Tests.Services
{
    [TestFixture]
    public class PostServiceTests
    {
        private Mock<IPostRepository>? _mockPostRepository;
        private Mock<IMapper>? _mockMapper;
        private PostService? _service;

        [SetUp]
        public void Setup()
        {
            _mockPostRepository = new Mock<IPostRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new PostService(_mockPostRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task GetByIdAsync_PostExists_ReturnsPostResponse()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Test Post" };
            var postResponse = new PostResponse { Id = 1, Title = "Test Post" };

            _mockPostRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(post);
            _mockMapper.Setup(m => m.Map<PostResponse>(post)).Returns(postResponse);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.That(result, Is.EqualTo(postResponse));
        }

        [Test]
        public void GetByIdAsync_PostDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _mockPostRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Post?)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(1));
        }

        [Test]
        public async Task CreateAsync_ValidPost_ReturnsPostResponse()
        {
            // Arrange
            var postDto = new PostDTO { Title = "New Post", Content = "Content" };
            var post = new Post { Id = 1, Title = "New Post", Content = "Content" };
            var postResponse = new PostResponse { Id = 1, Title = "New Post", Content = "Content" };

            _mockMapper.Setup(m => m.Map<Post>(postDto)).Returns(post);
            _mockPostRepository.Setup(repo => repo.InsertAsync(post)).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<PostResponse>(post)).Returns(postResponse);

            // Act
            var result = await _service.CreateAsync(postDto, "Creator", 1);

            // Assert
            Assert.That(result, Is.EqualTo(postResponse));
        }

        [Test]
        public async Task UpdateAsync_PostExists_ReturnsUpdatedPostResponse()
        {
            // Arrange
            var postDto = new PostDTO { Title = "Updated Post", Content = "Updated Content" };
            var post = new Post { Id = 1, Title = "Old Post", Content = "Old Content" };
            var updatedPost = new Post { Id = 1, Title = "Updated Post", Content = "Updated Content" };
            var postResponse = new PostResponse { Id = 1, Title = "Updated Post", Content = "Updated Content" };

            _mockPostRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(post);
            _mockMapper.Setup(m => m.Map(postDto, post)).Returns(updatedPost);
            _mockPostRepository.Setup(repo => repo.UpdateAsync(updatedPost)).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<PostResponse>(updatedPost)).Returns(postResponse);

            // Act
            var result = await _service.UpdateAsync(1, postDto);

            // Assert
            Assert.That(result, Is.EqualTo(postResponse));
        }

        [Test]
        public void UpdateAsync_PostDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var postDto = new PostDTO { Title = "Updated Post", Content = "Updated Content" };
            _mockPostRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Post?)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateAsync(1, postDto));
        }

        [Test]
        public async Task DeleteAsync_PostExists_SetsIsDeletedToTrue()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Test Post", IsDeleted = false };

            _mockPostRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(post);
            _mockPostRepository.Setup(repo => repo.UpdateAsync(post)).Returns(Task.CompletedTask);

            // Act
            await _service.DeleteAsync(1);

            // Assert
            Assert.That(post.IsDeleted, Is.True);
        }

        [Test]
        public void DeleteAsync_PostDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _mockPostRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Post?)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(1));
        }
    }
}
