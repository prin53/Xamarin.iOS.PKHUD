using PKHUD.Views;
using UIKit;

namespace PKHUD
{
    public class ContentBuilder
    {
        public Builder WithContent(UIView content)
        {
            return new Builder(content);
        }
        
        public Builder WithSquareContent()
        {
            return new Builder(new SquareBaseView());
        }
        
        public Builder WithRotatingSquareContent()
        {
            return new Builder(new RotatingImageView());
        }

        public Builder WithSuccessContent()
        {
            return new Builder(new SuccessView());
        }

        public Builder WithErrorContent()
        {
            return new Builder(new ErrorView());
        }
        
        public Builder WithProgressContent()
        {
            return new Builder(new ProgressView());
        }
        
        public Builder WithSystemActivityContent()
        {
            return new Builder(new SystemActivityIndicatorView());
        }
        
        public Builder WithLabelContent()
        {
            return new Builder(new TextView());
        }
    }
}
