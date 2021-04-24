using System;
using FluentAssertions;
using Kalendra.BoardSystem.Runtime.Domain.Entities;
using Kalendra.BoardSystem.Runtime.Domain.Entities.BoardOperations;
using Kalendra.BoardSystem.Runtime.Domain.Policy;
using Kalendra.BoardSystem.Tests.TestDataBuilders.StaticShortcuts;
using NSubstitute;
using NUnit.Framework;

namespace Kalendra.BoardSystem.Tests.Editor
{
    public class MergeOperationTests
    {
        [Test]
        public void New_WhenOriginAndTargetTilesAreTheSame_ThrowsException()
        {
            var sameTile = Fake.Tile();
            MergeOperation sut;

            Action act = () => sut = Build.MergeOperation().WithOriginTile(sameTile).WithTargetTile(sameTile);
            
            act.Should().Throw<InvalidOperationException>();
        }

        #region IsAvailable
        [Test]
        public void IsAvailable_WhenMergeIsNotAvailableByPolicy_IsFalse()
        {
            //Arrange
            var notAvailableOperator = Substitute.For<IMergeOperatorPolicy>();
            notAvailableOperator.IsAvailable(default).ReturnsForAnyArgs(false);

            var someboard = Build.Board().Build();

            MergeOperation sut = Build.MergeOperation().WithPolicy(notAvailableOperator);
            
            //Act
            var resultIsAvailable = sut.IsAvailable(someboard);

            //Assert
            resultIsAvailable.Should().BeFalse();
        }
        #endregion

        #region Execute
        [Test]
        public async void Execute_SetsMergedContent_InTargetTile()
        {
            //Arrange
            var someboard = Build.Board().WithSize(1, 2).Build();
            
            var someContent = Fake.TileContent_NotNull();
            var mergeOperatorMock = Substitute.For<IMergeOperatorPolicy>();
            mergeOperatorMock.IsAvailable(default).ReturnsForAnyArgs(true);
            mergeOperatorMock.Merge(default).ReturnsForAnyArgs(someContent);

            MergeOperation sut = Build.MergeOperation().WithTargetTile(someboard[0, 1]).WithPolicy(mergeOperatorMock);
            
            //Act
            await sut.Execute(someboard);
            
            //Assert
            someboard[0, 1].Content.Should().Be(someContent);
        }
        
        [Test]
        public async void Execute_DestroysOriginContent_InOriginTile()
        {
            //Arrange
            var someboard = Build.Board().WithSize(1, 2).Build();
            someboard[0, 0].Content = Fake.TileContent_NotNull();
            
            var someContent = Fake.TileContent_NotNull();
            var mergeOperatorMock = Substitute.For<IMergeOperatorPolicy>();
            mergeOperatorMock.IsAvailable(default).ReturnsForAnyArgs(true);
            mergeOperatorMock.Merge(default).ReturnsForAnyArgs(someContent);

            MergeOperation sut = Build.MergeOperation().WithOriginTile(someboard[0, 0]).WithPolicy(mergeOperatorMock);
            
            //Act
            await sut.Execute(someboard);
            
            //Assert
            someboard[0, 0].Content.Should().BeOfType<NullTileContent>();
        }

        [Test]
        public async void Execute_Twice_MergesTheSameContent()
        {
            //Arrange
            var someboard = Build.Board().WithSize(1, 2).Build();
            
            var mergeOperatorMock = Substitute.For<IMergeOperatorPolicy>();
            mergeOperatorMock.IsAvailable(default).ReturnsForAnyArgs(true);
            mergeOperatorMock.Merge(default).ReturnsForAnyArgs(Fake.TileContent_NotNull(), Fake.TileContent_NotNull());

            MergeOperation sut = Build.MergeOperation().WithTargetTile(someboard[0, 1]).WithPolicy(mergeOperatorMock);
            
            //Act
            await sut.Execute(someboard);
            var expectedContent = someboard[0, 1].Content;
            
            await sut.Undo(someboard);
            
            await sut.Execute(someboard);
            
            //Assert
            someboard[0, 1].Content.Should().Be(expectedContent);
        }
        #endregion
        
        #region Undo
        [Test]
        public async void Undo_WhenPastContentWasNull_ThenNullContentIsRestored_InTargetTile()
        {
            //Arrange
            var someboard = Build.Board().WithSize(1, 2).Build();
            
            var someContent = Fake.TileContent_NotNull();
            var mergeOperatorMock = Substitute.For<IMergeOperatorPolicy>();
            mergeOperatorMock.IsAvailable(default).ReturnsForAnyArgs(true);
            mergeOperatorMock.Merge(default).ReturnsForAnyArgs(someContent);

            MergeOperation sut = Build.MergeOperation().WithTargetTile(someboard[0, 1]).WithPolicy(mergeOperatorMock);
            
            //Act
            await sut.Execute(someboard);
            await sut.Undo(someboard);
            
            //Assert
            someboard[0, 1].Content.Should().BeOfType<NullTileContent>();
        }
        
        [Test]
        public async void Undo_PastContentIsRestored_InTargetTile()
        {
            //Arrange
            var pastContent = Fake.TileContent_NotNull();
            var someboard = Build.Board().WithSize(1, 2).Build();
            someboard[0, 1].Content = pastContent;
            
            var someContent = Fake.TileContent_NotNull();
            var mergeOperatorMock = Substitute.For<IMergeOperatorPolicy>();
            mergeOperatorMock.IsAvailable(default).ReturnsForAnyArgs(true);
            mergeOperatorMock.Merge(default).ReturnsForAnyArgs(someContent);

            MergeOperation sut = Build.MergeOperation().WithTargetTile(someboard[0, 1]).WithPolicy(mergeOperatorMock);
            
            //Act
            await sut.Execute(someboard);
            await sut.Undo(someboard);
            
            //Assert
            someboard[0, 1].Content.Should().Be(pastContent);
        }
        
        [Test]
        public async void Undo_OriginContentIsRestored_InOriginTile()
        {
            //Arrange
            var pastContent = Fake.TileContent_NotNull();
            var someboard = Build.Board().WithSize(1, 2).Build();
            someboard[0, 0].Content = pastContent;
            
            var someContent = Fake.TileContent_NotNull();
            var mergeOperatorMock = Substitute.For<IMergeOperatorPolicy>();
            mergeOperatorMock.IsAvailable(default).ReturnsForAnyArgs(true);
            mergeOperatorMock.Merge(default).ReturnsForAnyArgs(someContent);

            MergeOperation sut = Build.MergeOperation().WithOriginTile(someboard[0, 0]).WithPolicy(mergeOperatorMock);
            
            //Act
            await sut.Execute(someboard);
            await sut.Undo(someboard);
            
            //Assert
            someboard[0, 0].Content.Should().Be(pastContent);
        }
        #endregion
    }
}