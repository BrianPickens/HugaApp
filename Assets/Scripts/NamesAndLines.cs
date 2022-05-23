using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamesAndLines : MonoBehaviour
{
    private static NamesAndLines instance = null;
    public static NamesAndLines Instance { get { return instance; } }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    public string GetAnimalName(Animals _type)
    {
        string name = "";

        switch (_type)
        {
            case Animals.Porcupine:
                name = "ASAP Pine";
                break;

            case Animals.Bear:
                name = "Amelia Bearhart";
                break;

            case Animals.Octopus:
                name = "Ocatavia Ocho";
                break;

            case Animals.Crocodile:
                name = "Audrey Crocs";
                break;

            case Animals.Hippo:
                name = "Mr. Potamus";
                break;

            case Animals.Gorilla:
                name = "Gordon Von Rilla";
                break;

            case Animals.Jaguar:
                name = "Rodrigo";
                break;

            case Animals.Giraffe:
                name = "Salvador Raffe";
                break;

            case Animals.Caesar:
                name = "Caesar";
                break;

            case Animals.Dolphin:
                name = "Daphne Fins";
                break;

            case Animals.Bees:
                name = "Beatrice Combs";
                break;

            case Animals.Duck:
                name = "Sarah Honklin";
                break;

            case Animals.Snake:
                name = "Eve Serpentina";
                break;

            case Animals.Shark:
                name = "Bubba Sharp";
                break;

            case Animals.Catfish:
                name = "Neko Sakana";
                break;

            case Animals.Eagle:
                name = "Mason Flier";
                break;

            case Animals.Ants:
                name = "The Chicago Carpenters";
                break;

            case Animals.Spider:
                name = "Tony Bromo";
                break;

            case Animals.Jellyfish:
                name = "Luminous James";
                break;

            case Animals.Bunny:
                name = "Harrient Hops";
                break;

            case Animals.Krill:
                name = "Stanley Shrimpton";
                break;

            case Animals.Dog:
                name = "Corgi Minaj";
                break;

            case Animals.Skunk:
                name = "Ashley Smellums";
                break;

            case Animals.Squirrel:
                name = "Nikolay Alexi";
                break;

            case Animals.Badger:
                name = "Charles Badgington";
                break;

            default:
                name = "Missing Name";
                break;
        }

        return name;
    }

    public string GetAnimalLine(Animals _type)
    {
        string line = "";

        switch (_type)
        {
            case Animals.Porcupine:
                line = "You must be my new album, becuase you're fire!";
                break;

            case Animals.Bear:
                line = "You can circumnavigate me anytime!";
                break;

            case Animals.Octopus:
                line = "I'm a sucker for you!";
                break;

            case Animals.Crocodile:
                line = "You won't be saying see you later to this alligator! (because I'm a crocodile)";
                break;

            case Animals.Hippo:
                line = "You look like a bank account becuase you've compounded my interest!";
                break;

            case Animals.Gorilla:
                line = "I say! What ho! What ho!";
                break;

            case Animals.Jaguar:
                line = "Hola mi Amor. Que Tal?";
                break;

            case Animals.Giraffe:
                line = "You're so fine you could make an impression on Monet.";
                break;

            case Animals.Caesar:
                line = "I wouldn't steal your chair.";
                break;

            case Animals.Dolphin:
                line = "Let's be bad on porpoise!";
                break;

            case Animals.Bees:
                line = "Do you work with bees? Because you're definitely a keeper!";
                break;

            case Animals.Duck:
                line = "Wanna meet up for cheese and quackers?";
                break;

            case Animals.Snake:
                line = "Are you a snake charmer? Cause I find you fassssssinating.";
                break;

            case Animals.Shark:
                line = "I think you're Jawsome!";
                break;

            case Animals.Catfish:
                line = "I swear my pic is real!";
                break;

            case Animals.Eagle:
                line = "I showed you my fish, plz respond!";
                break;

            case Animals.Ants:
                line = "We're down to play 'ball' if you are?";
                break;

            case Animals.Spider:
                line = "I promise I'm at least 6 feet!";
                break;

            case Animals.Jellyfish:
                line = "You must be a basketball hoop, because I'd take a shot at you from anywhere!";
                break;

            case Animals.Bunny:
                line = "No bunny compares to you!";
                break;

            case Animals.Krill:
                line = "I've calculated our chance of cuddling success to be 100%!";
                break;

            case Animals.Dog:
                line = "You're barking up the right tree!";
                break;

            case Animals.Skunk:
                line = "I'll raise a stink if you don't message me back!";
                break;

            case Animals.Squirrel:
                line = "Previet, would you like to Rush B with me?";
                break;

            case Animals.Badger:
                line = "I'm not sure what you've heard, but this honey badger does care ;)";
                break;

            default:
                line = "MISSING LINE";
                break;
        }

        return line;
    }

}
