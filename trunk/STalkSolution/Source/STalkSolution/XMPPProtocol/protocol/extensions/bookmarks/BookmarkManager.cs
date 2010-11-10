
using System;

using XMPPProtocol.Protocol.client;

namespace XMPPProtocol.Protocol.extensions.bookmarks
{
    public class BookmarkManager
    {
        private XmppClientConnection	m_connection	= null;

        
        public BookmarkManager(XmppClientConnection con)
        {
            m_connection = con;
        }
        
        #region << Request Bookmarks >>
        /// <summary>
        /// Request the bookmarks from the storage on the server
        /// </summary>
        public void RequestBookmarks()
        {
            RequestBookmarks(null, null);
        }

        /// <summary>
        /// Request the bookmarks from the storage on the server
        /// </summary>
        /// <param name="cb"></param>
        public void RequestBookmarks(IqCB cb)
        {
            RequestBookmarks(cb, null);
        }

        /// <summary>
        /// Request the bookmarks from the storage on the server
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="cbArgs"></param>
        public void RequestBookmarks(IqCB cb, object cbArgs)
        {
            StorageIq siq = new StorageIq(IqType.get);
                      
            if (cb == null)
                m_connection.Send(siq);
            else
                m_connection.IqGrabber.SendIq(siq, cb, cbArgs);
        }
        #endregion


        #region << Store Bookmarks >>
        /// <summary>
        /// Send booksmarks to the server storage
        /// </summary>
        /// <param name="urls"></param>
        public void StoreBookmarks(Url[] urls)
        {
            StoreBookmarks(urls, null, null, null);
        }

        /// <summary>
        /// Send booksmarks to the server storage
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="cb"></param>
        public void StoreBookmarks(Url[] urls, IqCB cb)
        {
            StoreBookmarks(urls, null, cb, null);
        }

        /// <summary>
        /// Send booksmarks to the server storage
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="cb"></param>
        /// <param name="cbArgs"></param>
        public void StoreBookmarks(Url[] urls, IqCB cb, object cbArgs)
        {
            StoreBookmarks(urls, null, cb, cbArgs);
        }

        /// <summary>
        /// Send booksmarks to the server storage
        /// </summary>
        /// <param name="conferences"></param>
        public void StoreBookmarks(Conference[] conferences)
        {
            StoreBookmarks(null, conferences, null, null);
        }

        /// <summary>
        /// Send booksmarks to the server storage
        /// </summary>
        /// <param name="conferences"></param>
        /// <param name="cb"></param>
        public void StoreBookmarks(Conference[] conferences, IqCB cb)
        {
            StoreBookmarks(null, conferences, cb, null);
        }

        /// <summary>
        /// Send booksmarks to the server storage
        /// </summary>
        /// <param name="conferences"></param>
        /// <param name="cb"></param>
        /// <param name="cbArgs"></param>
        public void StoreBookmarks(Conference[] conferences, IqCB cb, object cbArgs)
        {
            StoreBookmarks(null, conferences, cb, cbArgs);
        }

        /// <summary>
        /// Send booksmarks to the server storage
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="conferences"></param>
        public void StoreBookmarks(Url[] urls, Conference[] conferences)
        {
            StoreBookmarks(urls, conferences, null, null);
        }

        /// <summary>
        /// Send booksmarks to the server storage
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="conferences"></param>
        /// <param name="cb"></param>
        public void StoreBookmarks(Url[] urls, Conference[] conferences, IqCB cb)
        {
            StoreBookmarks(urls, conferences, cb, null);
        }

        /// <summary>
        /// Send booksmarks to the server storage
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="conferences"></param>
        /// <param name="cb"></param>
        /// <param name="cbArgs"></param>
        public void StoreBookmarks(Url[] urls, Conference[] conferences, IqCB cb, object cbArgs)
        {
            StorageIq siq = new StorageIq(IqType.set);
            
            if (urls != null)
                siq.Query.Storage.AddUrls(urls);

            if (conferences != null)
                siq.Query.Storage.AddConferences(conferences);

            if (cb == null)
                m_connection.Send(siq);
            else
                m_connection.IqGrabber.SendIq(siq, cb, cbArgs);
        }
        #endregion
    }
}
