using System;
using System.Collections.Generic;
using System.Text;

namespace IoT_TestPublisher.Entities
{
    class IoTMessage
    {
        public string DeviceId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Status { get; set; }
        public string message { get; set; }
    }

    enum IoTStatus
    {
        Failure = 0,
        Alive = 1,
        ShuttingDown = 2
    }
}
