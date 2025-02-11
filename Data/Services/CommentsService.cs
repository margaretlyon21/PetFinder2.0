﻿using Microsoft.EntityFrameworkCore;
using PetFinder2._0.Models;

namespace PetFinder2._0.Data.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ApplicationDbContext _context;

        public CommentsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

        }
    }
}
