using System.Collections.Generic;
using System.Windows.Controls;

namespace Good_Teacher.Class.Serialization.Content_Ser
{
    public class ContentCreator
    {
        public delegate void CreatedDelegate();
        public event CreatedDelegate ContentCreated;

        public List<Content_Default> contents = new List<Content_Default>();

        public StackPanel Create(DataStore data)
        {
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;

            foreach(Content_Default conD in contents)
            {
                if(conD.ContentType() == 0)
                {
                    Content_Image img = (Content_Image)conD;
                    panel.Children.Add( img.Create(data));

                }else if(conD.ContentType() == 1)
                {
                    Content_Text text = (Content_Text)conD;
                    panel.Children.Add( text.Create(data));
                }
                else if (conD.ContentType() == 2)
                {
                    Content_Splitter split = (Content_Splitter)conD;
                    panel.Children.Add(split.Create(data));
                }
                else if (conD.ContentType() == 3)
                {
                    Content_PageNumber content = (Content_PageNumber)conD;
                    panel.Children.Add(content.Create(data));
                }
                else if (conD.ContentType() == 4)
                {
                    Content_Answers content = (Content_Answers)conD;
                    panel.Children.Add(content.Create(data));
                }
                else if (conD.ContentType() == 5)
                {
                    Content_Date content = (Content_Date)conD;
                    panel.Children.Add(content.Create(data));
                }
            }

            ContentCreated?.Invoke();

            return panel;
        }


        public string GetText()
        {
            string text = "";

            int k = 0;

            foreach(Content_Default txt in contents)
            {
                if (txt is Content_Text)
                {
                    if (k == 0)
                    {
                        text += ((Content_Text)txt).text;
                    }
                    else
                    {
                        text += " " + ((Content_Text)txt).text;
                    }

                    k++;
                }
            }

            return text;
        }

    }
}
