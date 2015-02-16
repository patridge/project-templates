using System;
using System.Runtime.InteropServices;
using Social;
using MessageUI;
using ObjCRuntime;
using Twitter;
using UIKit;

namespace ${SafeProjectName}.Helpers {
    public static class DeviceCapabilities {
        static string systemVersion = UIDevice.CurrentDevice.SystemVersion;
        public static string SystemVersion {
            get {
                return systemVersion;
            }
        }

        public enum DeviceFormFactor {
            Tablet,
            Phone
        }
        static DeviceFormFactor AsDeviceFormFactor(this UIUserInterfaceIdiom interfaceIdiom) {
            DeviceFormFactor result = DeviceFormFactor.Phone;
            if (interfaceIdiom == UIUserInterfaceIdiom.Pad) {
                result = DeviceFormFactor.Tablet;
            }
            return result;
        }
        static DeviceFormFactor formFactor = UIDevice.CurrentDevice.UserInterfaceIdiom.AsDeviceFormFactor();
        public static DeviceFormFactor FormFactor {
            get {
                return formFactor;
            }
        }
        public static bool IsTablet {
            get {
                return formFactor == DeviceFormFactor.Tablet;
            }
        }
        public static bool IsHighDpi {
            get {
                return UIScreen.MainScreen.Scale > 1.0f;
            }
        }

        public static bool IsEmailAvailable(this UIDevice device) {
            return device.CheckSystemVersion(3, 0) && MFMailComposeViewController.CanSendMail;
        }

        public static bool IsFacebookAvailable(this UIDevice device) {
            return device.CheckSystemVersion(6, 0) && SLComposeViewController.IsAvailable(SLServiceKind.Facebook);
        }

        public static bool IsSLComposeViewControllerAvailable(this UIDevice device) {
            return device.CheckSystemVersion(6, 0);
        }

        public static bool IsTWTweetComposeViewControllerAvailable(this UIDevice device) {
            return device.CheckSystemVersion(5, 0);
        }

        public static bool IsTwitterAvailable(this UIDevice device) {
            return device.CheckSystemVersion(5, 0) && TWTweetComposeViewController.CanSendTweet;
        }

        public static bool IsIOS41OrBetter {
            get {
                return UIDevice.CurrentDevice.CheckSystemVersion(4, 1);
            }
        }
        public static bool IsIOS5OrBetter {
            get {
                return UIDevice.CurrentDevice.CheckSystemVersion(5, 0);
            }
        }
        public static bool IsIOS6OrBetter {
            get {
                return UIDevice.CurrentDevice.CheckSystemVersion(6, 0);
            }
        }
        public static bool IsIOS7OrBetter {
            get {
                return UIDevice.CurrentDevice.CheckSystemVersion(7, 0);
            }
        }
        public static bool IsIOS8OrBetter {
            get {
                return UIDevice.CurrentDevice.CheckSystemVersion(8, 0);
            }
        }

        /// <summary>
        /// Adapted from Xamarin wiki scrape (wiki no longer exists): http://www.dzone.com/snippets/monotouch-get-hardware-version.
        /// </summary>
        public class DeviceHardware {
            public const string HardwareProperty = "hw.machine";

            [DllImport(Constants.SystemLibrary)]
            internal static extern int sysctlbyname([MarshalAs(UnmanagedType.LPStr)] string property, // name of the property
                IntPtr output, // output
                IntPtr oldLen, // IntPtr.Zero
                IntPtr newp, // IntPtr.Zero
                uint newlen // 0
            );

            public static string Version {
                get {
                    string version = "unknown";
                    // get the length of the string that will be returned
                    var pLen = Marshal.AllocHGlobal(sizeof(int));
                    sysctlbyname(DeviceHardware.HardwareProperty, IntPtr.Zero, pLen, IntPtr.Zero, 0);

                    var length = Marshal.ReadInt32(pLen);

                    // check to see if we got a length
                    if (length == 0) {
                        Marshal.FreeHGlobal(pLen);
                        return version;
                    }

                    // get the hardware string
                    var pStr = Marshal.AllocHGlobal(length);
                    sysctlbyname(DeviceHardware.HardwareProperty, pStr, pLen, IntPtr.Zero, 0);

                    // convert the native string into a C# string
                    version = Marshal.PtrToStringAnsi(pStr);

                    // cleanup
                    Marshal.FreeHGlobal(pLen);
                    Marshal.FreeHGlobal(pStr);

                    return version;
                }
            }
        }
    }
}
