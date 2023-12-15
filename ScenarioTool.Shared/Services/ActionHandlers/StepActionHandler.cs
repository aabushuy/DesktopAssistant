using ScenarioTool.Shared.Entity;
using System.Collections.ObjectModel;

namespace ScenarioTool.Shared.Services.ActionHandlers
{
    internal abstract class StepActionHandler : IStepActionHandler
    {
        private bool _isValidated = false;        
        protected ScenarioStep? Step { get; private set; }
        protected List<Exception> ValidationErrors => new();

        public IReadOnlyCollection<Exception> Errors => new ReadOnlyCollection<Exception>(ValidationErrors);

        public async Task Init(ScenarioStep step)
        {
            Step = step;            
            _isValidated = false;
            ValidationErrors.Clear();

            await InnerInit(step);
        }

        public async Task<bool> Validate()
        {
            ValidationErrors.Clear();

            try
            {
                _isValidated = Step != null && await InnerValidate();
            }
            catch (Exception ex)
            {
                _isValidated = false;
                ValidationErrors.Add(ex);
            }

            return _isValidated && ValidationErrors.Count == 0;
        }

        public async Task Execute()
        {
            if (_isValidated)
            {
                _isValidated = false;

                await InnerExecute();
            }
            else 
            {
                throw new InvalidOperationException("Step is not validated.");
            }
        }

        protected virtual Task InnerInit(ScenarioStep step) => Task.CompletedTask;

        protected virtual Task<bool> InnerValidate() => Task.FromResult(true);

        protected virtual Task InnerExecute() => Task.CompletedTask;
    }
}
