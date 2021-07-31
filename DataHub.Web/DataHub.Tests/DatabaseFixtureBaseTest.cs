using System;
using System.Transactions;
using Xunit;

namespace DataHub.Tests
{
    public abstract class DatabaseFixtureBaseTest : IDisposable, IClassFixture<DatabaseFixture>
    {
        private readonly TransactionScope _transactionScope;

        protected DatabaseFixture DatabaseFixture { get; }

        protected DatabaseFixtureBaseTest(DatabaseFixture databaseFixture)
        {
            DatabaseFixture = databaseFixture;
            _transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        }

        public void Dispose()
        {
            _transactionScope.Dispose();
        }
    }
}
