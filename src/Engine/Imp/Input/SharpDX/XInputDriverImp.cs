﻿using System.Collections.Generic;
using SharpDX.XInput;

namespace Fusee.Engine
{
    /// <summary>
    /// Sharp DX (Microsoft XInput) specific implementation for the <see cref="IXInputDriverImp"/>.
    /// </summary>
    class XInputDriverImp : IXInputDriverImp
    {
        public List<Controller> Devices = new List<Controller>();

        /// <summary>
        /// All SharpDX (Microsoft XInput) compatible input devices are initialised and added to a List of the type <see cref="IInputDeviceImp"./>
        /// </summary>
        /// <returns>A list containing all XInput compatible input devices.</returns>
        public List<IXInputDeviceImp> DeviceImps()
        {
            // TODO: This is a bit complex. Could be done easier but then it is also less generic.
            var val = UserIndex.GetValues(typeof(UserIndex));

            // Loop over the enum and check for every user id.
            foreach (UserIndex userid in val)
            {
                if(userid == UserIndex.Any)
                    continue;

                Devices.Add(new Controller(userid));
            }
            
            var retList = new List<IXInputDeviceImp>();
            foreach (Controller device in Devices)
            {
                retList.Add(new XInputDeviceImp(device));
            }
            return retList;
        }
    }
}