using CoreGraphics;
using UIKit;

namespace ${SafeProjectName}.Controllers {
    public class ${SafeProjectName}ViewController : BaseViewController {
        UITextView someView;
        public override void ViewDidLoad() {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.White;

            someView = new UITextView() {
                BackgroundColor = UIColor.LightGray,
                Text = "This is a test UITextField",
            };
            someView.SizeToFit();
            someView.Frame = new CGRect(new CGPoint(6f, 6f), someView.Frame.Size);
            Add(someView);
        }
    }
}
