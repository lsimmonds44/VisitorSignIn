using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class VisitorManager : IVisitorManager
    {
        public bool InsertVisitor(Visitor visitor)
        {
            try
            {
                visitor.SignedInTime = DateTime.Now;
                VisitorAccessor.CreateVisitor(visitor);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Visitor> RetrieveSignedIn()
        {
            List<Visitor> signedIn = null;
            try
            {
                signedIn = VisitorAccessor.RetrieveSignedInList(true);
            }
            catch (Exception)
            {
                throw;
            }
            return signedIn;
        }

        public Visitor RetrieveVisitorByID(int visitorID)
        {
            Visitor visitor = null;

            try
            {
                visitor = VisitorAccessor.RetrieveVisitor(visitorID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was a problem retrieving visitor information.", ex);
            }
            return visitor;
        }

        public bool SignOutVisitor(int visitorID)
        {
            try
            {
                return (VisitorAccessor.VisitorSignOut(visitorID) == 1);
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Visitor> RetrieveSignedOut()
        {
            List<Visitor> signedOut = null;
            try
            {
                signedOut = VisitorAccessor.RetrieveSignedOutList(true);
            }
            catch (Exception)
            {
                throw;
            }
            return signedOut;
        }
    }
}
