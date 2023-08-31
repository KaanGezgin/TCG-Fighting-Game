# TCG-Fighting-Game

This repository for implementing a TCG mechanics to fighting games

In this repository there is a little tool for controllings all variables in game like deck managment, card's attributes, player and oppoenets health's, hand sizes etc.
While you testing a game in play mode if you need changing values and test like changed values, tool give you all values in game so you have everything in your hand.
In this way, the test time is reduced and its effectiveness is increased.

The TCG part mechanics: 

You draw your cards and when you pass your turn discaring all cards in your hand to discard pile.

When there is a no card for draw in your deck, algortihm returning your discarded cards to deck again again and if you dont have enough cards in your hand you draw missing cards from a new fillded deck.

There is a currency system for playing a cards. Each card cost is different from others. Each turn you gain maximum amount of your currency.

There is a 4 different cards in your deck.

-> Quick attack for extender low damage but start a comba counter.

-> Heavy attack for dealing a massive damage and ending a combo counter.

-> Jab and Sweep cards basicly same think with quick attack but there is a catch but this catch explains in fighting game mechanic part.

-> Defenese cards for gaining defense counters. In game we have defense bar for decreasing a damage come from a opponent

The fighting games part mechanics:

You have a health bar. When you take damage your health decreased that much.

You have movement counter. This counters for movement backwards and forward.

If you want attack you must close to enemy so you need move forward and you want defenese you need go backwards 

There is a stance mechanic in game. Sweep and jab attacks need specific stance. And if you stand a rigth stance you take no damage from a enemy.

We have a little AI for oppoenent.

AI basicly choose its movements randomly. In AI script there is switch case block and randomizer.

Swich case block movements:

-> Changing its stance and attack

-> Without changing its stance and attack

-> Changing its stance and move

-> Without changing its stance and move
