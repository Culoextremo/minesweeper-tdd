using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using NSubstitute;
using NUnit.Framework;
using Fake = Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts.Fake;

namespace Kalendra.BoardSystem.Tests.Editor
{
    public class BoardOperationsManagerTests
    {
        #region New
        [Test]
        public void New_ReceivesOptionalOperationCollection()
        {
            var operationMocks = Fake.BoardOperations(2);

            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(operationMocks);

            sut.Scheduled.Should().ContainInOrder(operationMocks[0], operationMocks[1]);
        }

        [Test]
        public void RegisterOperation_ThenOperationIsAdded()
        {
            var operationMock = Fake.BoardOperation();
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(operationMock);

            sut.RegisterOperation(operationMock);

            sut.Scheduled.Should().Contain(operationMock);
        }
        #endregion
        
        #region HasScheduled
        [Test]
        public void HasScheduled_IsFalse_ByDefault()
        {
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager();

            var result = sut.HasScheduled;

            result.Should().BeFalse();
        }
        
        [Test]
        public void HasScheduled_WhenNoScheduledLeft_IsFalse()
        {
            var someBoard = Build.Board().Build();
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(Fake.BoardOperation());
            sut.DoNext(someBoard);
            
            var result = sut.HasScheduled;

            result.Should().BeFalse();
        }

        [Test]
        public void HasScheduled_AfterRegisterAnOperation_IsTrue()
        {
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(Fake.BoardOperation());
            
            var result = sut.HasScheduled;

            result.Should().BeTrue();
        }
        
        [Test]
        public void HasScheduled_AfterDoLessOperationThanRegistered_IsTrue()
        {
            var someBoard = Build.Board().Build();
            var operationMocks = Fake.BoardOperations(2);
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(operationMocks);
            sut.DoNext(someBoard);
            
            var result = sut.HasScheduled;

            result.Should().BeTrue();
        }
        #endregion

        #region HasExecuted
        [Test]
        public void HasExecuted_ByDefault_IsFalse()
        {
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager();

            var result = sut.HasExecuted;

            result.Should().BeFalse();
        }

        [Test]
        public void HasExecuted_BeforeAnyExecuteOperation_IsFalse()
        {
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(Fake.BoardOperation());

            var result = sut.HasExecuted;

            result.Should().BeFalse();
        }

        [Test]
        public void HasExecuted_AfterExecute_IsTrue()
        {
            var someBoard = Build.Board().Build();
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(Fake.BoardOperation());
            
            sut.DoNext(someBoard);
            var result = sut.HasExecuted;

            result.Should().BeTrue();
        }

        [Test]
        public void HasExecuted_AfterUndoExecutedOperations_IsFalse()
        {
            var someBoard = Build.Board().Build();
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(Fake.BoardOperation());
            
            sut.DoNext(someBoard);
            sut.UndoLast(someBoard);
            var result = sut.HasExecuted;

            result.Should().BeFalse();
        }
        #endregion

        #region HasUndone
        [Test]
        public void HasUndone_IsFalse_ByDefault()
        {
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(Fake.BoardOperation());

            var result = sut.HasUndone;

            result.Should().BeFalse();
        }

        [Test]
        public void HasUndone_AfterUndone_IsTrue()
        {
            var someBoard = Build.Board().Build();
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(Fake.BoardOperation());

            sut.DoNext(someBoard);
            sut.UndoLast(someBoard);
            var result = sut.HasUndone;

            result.Should().BeTrue();
        }
        #endregion

        #region Do
        [Test]
        public void DoNext_CallsOperationExecution()
        {
            var someBoard = Build.Board().Build();
            var operationMock = Fake.BoardOperation();
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(operationMock);

            sut.DoNext(someBoard);

            operationMock.Received().Execute(someBoard);
        }

        [Test]
        public void DoNext_Twice_RegisteredTwoOperations()
        {
            var someBoard = Build.Board().Build();
            var operationMocks = Fake.BoardOperations(2);
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(operationMocks);

            sut.DoNext(someBoard);
            sut.DoNext(someBoard);

            operationMocks[0].Received().Execute(someBoard);
            operationMocks[1].Received().Execute(someBoard);
            
        }
        #endregion

        #region Undo
        [Test]
        public void UndoLast_AfterDoAnOperation_ThenUndoThatOperation()
        {
            var someBoard = Build.Board().Build();
            var operationMock = Fake.BoardOperation();
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(operationMock);

            sut.DoNext(someBoard);
            sut.UndoLast(someBoard);

            operationMock.Received().Undo(someBoard);
        }

        [Test]
        public void UndoLast_DoesNothing_OnNotExecutedOperations()
        {
            //Arrange
            var someBoard = Build.Board().Build();
            var notExecutedOperationMock = Fake.BoardOperation();

            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(Fake.BoardOperation());
            sut.DoNext(someBoard);
            sut.RegisterOperation(notExecutedOperationMock);

            //Act
            sut.UndoLast(someBoard);
            sut.UndoLast(someBoard);

            //Assert
            notExecutedOperationMock.DidNotReceive().Undo(someBoard);
        }
        #endregion

        #region Redo
        [Test]
        public void RedoLast_DoesNothing_IfNoOperationWasDone()
        {
            //Arrange
            var someBoard = Build.Board().Build();
            var notDoneOperationMock = Fake.BoardOperation();
            
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(notDoneOperationMock);
            
            //Act
            sut.RedoLast(someBoard);

            //Assert
            notDoneOperationMock.DidNotReceive().Execute(someBoard);
        }
        
        [Test]
        public void RedoLast_DoesNothing_IfNoOperationWasUndone()
        {
            //Arrange
            var someBoard = Build.Board().Build();
            var notUndoneOperationMock = Fake.BoardOperation();
            
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(notUndoneOperationMock);
            sut.DoNext(someBoard);
            notUndoneOperationMock.ClearReceivedCalls();
                        
            //Act
            sut.RedoLast(someBoard);

            //Assert
            notUndoneOperationMock.DidNotReceive().Execute(someBoard);
        }
        
        [Test]
        public void RedoLast_WhenOperationWasUndone_CallsThatOperationExecution()
        {
            //Arrange
            var someBoard = Build.Board().Build();
            var undoneOperationMock = Fake.BoardOperation();
            
            BoardAsyncOperationScheduler sut = Build.BoardOperationsManager().WithOperations(undoneOperationMock);
            sut.DoNext(someBoard);
            sut.UndoLast(someBoard);
            undoneOperationMock.ClearReceivedCalls();
                        
            //Act
            sut.RedoLast(someBoard);

            //Assert
            undoneOperationMock.Received().Execute(someBoard);
        }
        #endregion
    }
}