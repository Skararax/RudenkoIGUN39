using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Card 
{ 
    public CardSuit Suit { get; }
    public CardRank Rank { get; }

    public Card(CardSuit suit, CardRank rank) 
    { 
        Suit = suit;
        Rank = rank;    
    }

} 
