using AutoMapper;
using Blog.Application.DTOs.CommentDTOs;
using Blog.Application.IRepositories;
using Blog.Application.Services.CommentServices;
using Blog.Domain.Entities;
using Blog.Domain.Exceptions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Tests.Services
{
    [TestFixture]
    public class CommentServiceTests
    {
        private Mock<ICommentRepository>? _mockCommentRepository;
        private Mock<IMapper>? _mockMapper;
        private CommentService? _service;

        [SetUp]
        public void Setup()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new CommentService(_mockCommentRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task CreateCommentAsync_ValidComment_ReturnsCommentResponse()
        {
            // Arrange
            var commentDto = new CommentDTO { Content = "New Comment", PostId = 1 };
            var comment = new Comment { Id = 1, Content = "New Comment", PostId = 1, CreatedAt = DateTime.UtcNow, CreatedBy = "Commenter" };
            var commentResponse = new CommentResponse { PostId = 1, Content = "New Comment", CreatedAt = comment.CreatedAt.Value, CreatedBy = "Commenter" };

            _mockCommentRepository.Setup(repo => repo.InsertAsync(It.IsAny<Comment>())).Returns(Task.CompletedTask);
            _mockCommentRepository.Setup(repo => repo.SaveChangeAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<CommentResponse>(It.IsAny<Comment>())).Returns(commentResponse);

            // Act
            var result = await _service.CreateCommentAsync(commentDto);

            // Assert
            _mockCommentRepository.Verify(repo => repo.InsertAsync(It.IsAny<Comment>()), Times.Once);
            _mockCommentRepository.Verify(repo => repo.SaveChangeAsync(), Times.Once);
            _mockMapper.Verify(m => m.Map<CommentResponse>(It.IsAny<Comment>()), Times.Once);
            Assert.That(result, Is.EqualTo(commentResponse));
        }

        [Test]
        public async Task GetCommentByIdAsync_CommentExists_ReturnsCommentResponse()
        {
            // Arrange
            var comment = new Comment { Id = 1, Content = "Test Comment", PostId = 1 };
            var commentResponse = new CommentResponse { PostId = 1, Content = "Test Comment" };

            _mockCommentRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(comment);
            _mockMapper.Setup(m => m.Map<CommentResponse>(comment)).Returns(commentResponse);

            // Act
            var result = await _service.GetCommentByIdAsync(1);

            // Assert
            Assert.That(result, Is.EqualTo(commentResponse));
        }

        [Test]
        public void GetCommentByIdAsync_CommentDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            _mockCommentRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Comment?)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _service.GetCommentByIdAsync(1));
        }

        //[Test]
        //public async Task GetCommentsByPostIdAsync_CommentsExist_ReturnsCommentResponses()
        //{
        //    // Arrange
        //    var postId = 1;
        //    var post = new Post { Id = 1, Title = "Test Post", Content = "Test Content" };
        //    var comments = new List<Comment>
        //    {
        //        new Comment { Id = 1, Content = "Comment 1", PostId = 1 },
        //        new Comment { Id = 2, Content = "Comment 2", PostId = 1 }
        //    };
        //            var commentResponses = new List<CommentResponse>
        //    {
        //        new CommentResponse { PostId = 1, Content = "Comment 1" },
        //        new CommentResponse { PostId = 1, Content = "Comment 2" }
        //    };

        //    //_mockCommentRepository.Setup(repo => repo.GetByIdAsync(postId)).ReturnsAsync(comments);
        //    _mockMapper.Setup(m => m.Map<IEnumerable<CommentResponse>>(comments)).Returns(commentResponses);

        //    // Act
        //    var result = await _service.GetCommentsByPostIdAsync(1);

        //    // Assert
        //    Assert.That(result, Is.EqualTo(commentResponses));
        //}


        //[Test]
        //public void GetCommentsByPostIdAsync_CommentsDoNotExist_ThrowsNotFoundException()
        //{
        //    // Arrange
        //    _mockCommentRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync((IEnumerable<Comment>?)null);

        //    // Act & Assert
        //    Assert.ThrowsAsync<NotFoundException>(() => _service.GetCommentsByPostIdAsync(1));
        //}
    }
}
