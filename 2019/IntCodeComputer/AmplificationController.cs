using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Aoc.AoC2019.IntCode
{
    public class AmplificationController
    {
        protected readonly List<long> _settings;
        protected readonly List<IntCodeVM> _compilers;

        public AmplificationController(IEnumerable<long> settings, IEnumerable<long> instructions)
        {
            _settings = settings?.ToList() ?? throw new ArgumentNullException(nameof(settings));

            _compilers = new List<IntCodeVM>(_settings.Count);
            for (int i = 0; i < _settings.Count; i++)
            {
                var c = new IntCodeVM(new List<long>(instructions), _settings[i]);
                _compilers.Add(c);
            }
        }

        public virtual long RunAmplifiedCircuits(long initialInput = 0)
        {
            long output = initialInput;
            foreach (IntCodeVM compiler in _compilers)
            {
                // add output to next compiler
                compiler.Execute(output);
                output = compiler.Outputs.Dequeue();
            }
            return output;
        }
    }

    public class FeedbackAmplificationController : AmplificationController
    {
        public FeedbackAmplificationController(IEnumerable<long> settings, IEnumerable<long> instructions) : base(settings, instructions) { }

        public override long RunAmplifiedCircuits(long initialInput = 0)
        {
            List<long> output = new List<long> { initialInput };

            while (true)
            {
                for (int ix = 0; ix < _compilers.Count; ix++)
                {                    
                    IntCodeVM compiler = _compilers[ix];

                    // add output to next compiler
                    compiler.Execute(output.ToArray());

                    output = compiler.Outputs.DequeueToList();

                    if (_compilers.All(c => c.Status == ExecutionStatus.Finished))
                    {
                        return output.Last();
                    }                  
                }
            }
        }
    }
}