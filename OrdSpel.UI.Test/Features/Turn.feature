
Feature: Word and game validation

Scenario: Player is submitting a valid word
Given the start word is "hund"
And it is my turn to play
And I submit the word "delfin"
Then I should get points for "delfin"