using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.Generic;

namespace AverageAssistant.Messengers
    {
    public class GradesListMessage : ValueChangedMessage<List<double>>
    {
        public GradesListMessage(List<double> cleanedData) : base(cleanedData)
        {
        }
    }
}