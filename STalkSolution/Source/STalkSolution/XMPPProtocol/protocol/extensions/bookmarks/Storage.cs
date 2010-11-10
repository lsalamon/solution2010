

using System;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Protocol.extensions.bookmarks
{
    /// <summary>
    /// 
    /// </summary>
    public class Storage : Element
    {
        /*
            <iq type='result' id='2'>
              <query xmlns='jabber:iq:private'>
                <storage xmlns='storage:bookmarks'>
                  <conference name='Council of Oberon' 
                              autojoin='true'
                              jid='council@conference.underhill.org'>
                    <nick>Puck</nick>
                    <password>titania</password>
                  </conference>
                </storage>
              </query>
            </iq>   
        */
        public Storage()
        {
            this.TagName    = "storage";
            this.Namespace  = Uri.STORAGE_BOOKMARKS;
        }
        
        /// <summary>
        /// Add a conference bookmark to the storage object
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        public Conference AddConference(Conference conf)
        {
            AddChild(conf);
            return conf;
        }

        /// <summary>
        /// Add a conference bookmark to the storage object
        /// </summary>
        /// <param name="jid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Conference AddConference(Jid jid, string name)
        {
            return AddConference(new Conference(jid, name));
        }

        /// <summary>
        /// Add a conference bookmark to the storage object
        /// </summary>
        /// <param name="jid"></param>
        /// <param name="name"></param>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public Conference AddConference(Jid jid, string name, string nickname)
        {
            return AddConference(new Conference(jid, name, nickname));
        }

        /// <summary>
        /// Add a conference bookmark to the storage object
        /// </summary>
        /// <param name="jid"></param>
        /// <param name="name"></param>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Conference AddConference(Jid jid, string name, string nickname, string password)
        {
            return AddConference(new Conference(jid, name, nickname, password));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jid"></param>
        /// <param name="name"></param>
        /// <param name="nickname"></param>
        /// <param name="password"></param>
        /// <param name="autojoin"></param>
        /// <returns></returns>
        public Conference AddConference(Jid jid, string name, string nickname, string password, bool autojoin)
        {
            return AddConference(new Conference(jid, name, nickname, password, autojoin));
        }

        /// <summary>
        /// add multiple conference bookmarks
        /// </summary>
        /// <param name="confs"></param>
        public void AddConferences(Conference[] confs)
        {
            foreach (Conference conf in confs)
            {
                AddConference(conf);
            }
        }

        /// <summary>
        /// get all conference booksmarks
        /// </summary>
        /// <returns></returns>
        public Conference[] GetConferences()
        {
            ElementList nl = SelectElements(typeof(Conference));
            Conference[] items = new Conference[nl.Count];
            int i = 0;
            foreach (Element e in nl)
            {
                items[i] = (Conference)e;
                i++;
            }
            return items;
        }

        /// <summary>
        /// add a url bookmark
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Url AddUrl(Url url)
        {
            AddChild(url);
            return url;
        }

        public Url AddUrl(string address, string name)
        {
            return AddUrl(new Url(address, name));            
        }

        /// <summary>
        /// add multiple url bookmarks
        /// </summary>
        /// <param name="urls"></param>
        public void AddUrls(Url[] urls)
        {
            foreach (Url url in urls)
            {
                AddUrl(url);
            }           
        }

        /// <summary>
        /// Get all url bookmarks
        /// </summary>
        /// <returns></returns>
        public Url[] GetUrls()
        {
            ElementList nl = SelectElements(typeof(Url));
            Url[] items = new Url[nl.Count];
            int i = 0;
            foreach (Element e in nl)
            {
                items[i] = (Url) e;
                i++;
            }
            return items;
        }
    }
}
