using Goat.Domain;
using Goat.View.Sprites;

namespace Goat.View.Controls;

public partial class PlayingCard : ContentView
{
	public static readonly BindableProperty _spriteProperty = BindableProperty.Create(nameof(Sprite), typeof(Sprite), typeof(PlayingCard));
	public static readonly BindableProperty _suitProperty = BindableProperty.Create(nameof(Suit), typeof(Suit), typeof(PlayingCard));
	public static readonly BindableProperty _rankProperty = BindableProperty.Create(nameof(Rank), typeof(Rank), typeof(PlayingCard));

	public Sprite Sprite
    {
		get => (Sprite)GetValue(_spriteProperty);
		set => SetValue(_spriteProperty, value);
    }

    public Suit Suit
    {
		get => (Suit)GetValue(_suitProperty);
		set => SetValue(_suitProperty, value);
    }

    public Rank Rank
    {
		get => (Rank)GetValue(_rankProperty);
		set => SetValue(_rankProperty, value);
    }

    public PlayingCard()
	{
		InitializeComponent();
	}
}