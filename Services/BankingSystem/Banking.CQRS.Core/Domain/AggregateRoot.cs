using Banking.CQRS.Core.Events;

namespace Banking.CQRS.Core.Domain
{
    /// <summary>
    /// Manages all the events on the system.
    /// </summary>
    public abstract class AggregateRoot
    {

        public string Id { get; set; } = string.Empty;

        public int Version { get; set; } = -1;

        private const string ApplyMethodName = "Apply";
        private readonly List<EventBase> _changes = new();


        public List<EventBase> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }


        public void ApplyChange(EventBase eventToApply, bool isNewEvent)
        {
            try
            {
                Type applyEventParameterType = eventToApply.GetType();

                var applyMethod = GetType().GetMethod(ApplyMethodName, new[] { applyEventParameterType });

                applyMethod?.Invoke(this, new object[] { eventToApply });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (isNewEvent)
                {
                    _changes.Add(eventToApply);
                }
            }
        }

        public void RaiseEvent(EventBase eventToRaise)
        {
            ApplyChange(eventToRaise, true);
        }


        public void ReplayEvents(IEnumerable<EventBase> events)
        {
            foreach (var eventToReplay in events)
            {
                ApplyChange(eventToReplay, false);
            }
        }

    }
}
