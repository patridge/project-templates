using System;
using Foundation;
using UIKit;

namespace ${SafeProjectName} {
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate {
        public override UIWindow Window { get; set; }
        UIViewController rootController;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            rootController = new ${SafeProjectName}ViewController();
            Window = new UIWindow(UIScreen.MainScreen.Bounds);
            Window.RootViewController = rootController;
            Window.MakeKeyAndVisible();
            return true;
        }
    }

    public class Application {
        static void Main(string[] args) {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            try {
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch (Exception ex) {
                HandleException(ex);
            }
        }
        static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e) {
            HandleException(e.ExceptionObject as Exception);
        }
        static void HandleException(Exception exception) {
            Console.WriteLine("Top-level exception: {0}", exception);
        }
    }
}
