using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Flashcards.Solomonlol.Data
{
    internal class UnitOfWork : IDisposable
    {
        private ApplicationContext _context = new ApplicationContext();
        private StackRepository _stackRepository;
        private FlashcardRepository _flashcardRepository;
        private SessionRepository _sessionRepository;

        public StackRepository Stacks
        {
            get
            {
                if (_stackRepository == null)
                    _stackRepository = new StackRepository(_context);
                return _stackRepository;
            }
        }

        public FlashcardRepository Flashcards
        {
            get
            {
                if (_flashcardRepository == null)
                    _flashcardRepository = new FlashcardRepository(_context);
                return _flashcardRepository;
            }
        }

        public SessionRepository Sessions
        {
            get
            {
                if (_sessionRepository == null)
                    _sessionRepository = new SessionRepository(_context);
                return _sessionRepository;
            }
        }

        public async Task Save()
        {
                await _context.SaveChangesAsync();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
