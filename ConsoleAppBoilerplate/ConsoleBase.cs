using System;
using AutoMapper;

namespace ConsoleAppBoilerplate
{
    public interface IConsoleBase
    {
        void Start();
    }

    public class ConsoleBase : IConsoleBase
    {
        private readonly IMapper _mapper;

        public ConsoleBase(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}