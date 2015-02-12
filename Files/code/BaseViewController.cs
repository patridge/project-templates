using UIKit;

namespace ${SafeProjectName}.Controllers {
    public class BaseViewController : UIViewController {
        public override void ViewDidLoad() {
            base.ViewDidLoad();

            if (RespondsToSelector(new Selector("setEdgesForExtendedLayout:"))) {
                EdgesForExtendedLayout = UIRectEdge.None;
            }
        }
    }
}
