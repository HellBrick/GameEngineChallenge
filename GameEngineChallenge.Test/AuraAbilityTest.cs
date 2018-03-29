using FluentAssertions;
using GameEngineChallenge.Abilities;
using GameEngineChallenge.Services;
using Xunit;

using static GameEngineChallenge.Test.AbilityBuilder;

namespace GameEngineChallenge.Test
{
	public class AuraAbilityTest
	{
		[Fact]
		public void PotentialTargetIsLeftAloneIfItAlreadyHasAuraRequisite()
		{
			bool inflictedAbilityExecuted = false;
			IActiveAbility inflictedAbility = CreateActiveAbility( CommonTickPhases.OffensiveAbilities, ( h, c ) => inflictedAbilityExecuted = true );

			Hero auraTarget = new Hero( team: default, initialHp: default, inflictedAbility );
			AuraAbility auraAbility = new AuraAbility( inflictedAbility, CreateTargeter( ( h, c ) => auraTarget ) );
			Hero auraProvider = new Hero( team: default, initialHp: default, auraAbility );

			GameContext context = new GameContext( new HeroService( new[] { auraTarget, auraProvider } ), new InputService(), new TimeService(), new SpaceService() );

			new TickExecutor().ExecuteTick( context );

			inflictedAbilityExecuted.Should().BeTrue();
			auraTarget.Requisites.Count.Should().Be( 1 );
			auraTarget.Requisites.Should().Contain( inflictedAbility );
		}

		[Fact]
		public void AuraRequisiteIsAppliedButRemovedByTheEndOfTheTick()
		{
			bool inflictedAbilityExecuted = false;
			IActiveAbility inflictedAbility = CreateActiveAbility( CommonTickPhases.OffensiveAbilities, ( h, c ) => inflictedAbilityExecuted = true );

			Hero auraTarget = new Hero( team: default, initialHp: default );
			AuraAbility auraAbility = new AuraAbility( inflictedAbility, CreateTargeter( ( h, c ) => auraTarget ) );
			Hero auraProvider = new Hero( team: default, initialHp: default, auraAbility );

			GameContext context = new GameContext( new HeroService( new[] { auraTarget, auraProvider } ), new InputService(), new TimeService(), new SpaceService() );
			new TickExecutor().ExecuteTick( context );

			inflictedAbilityExecuted.Should().BeTrue();
			auraTarget.Requisites.Count.Should().Be( 0 );
		}
	}
}
