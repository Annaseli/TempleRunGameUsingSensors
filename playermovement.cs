using UnityEngine;
using System;
using System.IO.Ports;

public class playermovement : MonoBehaviour
{
    public AudioSource audio;
    public AudioSource audiobeforespace;
    public float speed;
    public float rotationSpeed;
    public Animator anim;
    public float MoveDistance = 2f;
    bool check = true;

    private long playerPosition = 1;
    private bool runn;
    static SerialPort _serialPort = new SerialPort("COM3", 9600);
    private long prevSign = 0;
    private long prevMax = 0;
    private long counterNumOfSamples = 0;
    private long stepCounter = 0;
    private long curNegative = 0;
    private long curPositive = 0;
    private long prev0Pos = 0;
    private long prev1Pos = 0;
    private long prev0Neg = 0;
    private long prev1Neg = 0;
    private long counterSpecial = 0;
    private long prevSizeOfWavePos = 0;
    private long prevSizeOfWaveNeg = 0;
    private long sizeOfWavePos = 0;
    private long sizeOfWaveNeg = 0;
    private bool changeSign = false;
    private bool stopCountingSumOfWave = false;

    bool firstTime = true;
    public float prevForward = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        random_animations();
        _serialPort.Open();
    }

    public static void SetArduinoConnection()
    {

    }

    public long readPort()
    {
        string readFromPortStr = _serialPort.ReadLine();
        long readFromPortInt;
        long num;

        if (Int64.TryParse(readFromPortStr, out num))
        {
            readFromPortInt = num;
        }
        else
        {
            readFromPortInt = -2;           
        }
        return readFromPortInt;
    }

    public long signFunc(long delta)
    {
        if (delta > -1)
        {
            // positive
            return 1;
        }
        // negative
        return -1;
    }

    public long readArduino()
    {
        long fsrReadingDiff = readPort();
        long signnew = signFunc(fsrReadingDiff);
        long maxVal = Math.Abs(fsrReadingDiff);
        bool check = ((prevSign == signnew) || (prevSign == 0)) && !stopCountingSumOfWave;
        if (check)
        {
            prevSign = signnew;

            if (maxVal > prevMax)
            {
                prevMax = maxVal;
            }

            if (prevSign == 1 && sizeOfWavePos > 200)
            {
                stopCountingSumOfWave = true;
            }
            else if (prevSign == 1 && sizeOfWavePos <= 200)
            {
                sizeOfWavePos++;
            }

            if (prevSign == -1 && sizeOfWaveNeg > 200)
            {
                stopCountingSumOfWave = true;
            }
            else if (prevSign == -1 && sizeOfWavePos <= 200)
            {
                sizeOfWaveNeg++;
            }        
        }
        if (stopCountingSumOfWave || !check)
        {
            stopCountingSumOfWave = false;
            stepCounter++;
            if (prevSign > -1)
            {
                curPositive = prevMax;
            }
            else
            {
                curNegative = prevMax;
            }
            prevMax = 0;
            prevSign = signnew;
            if (stepCounter == 2)
            {
                long averageWaves;              
                long averagePos;
                long averageNeg;
                averagePos = curPositive;
                averageNeg = curNegative;
                averageWaves = sizeOfWavePos - sizeOfWaveNeg;
                long power = averagePos - averageNeg;
                prev0Pos = curPositive;
                prev0Neg = curNegative;
                stepCounter = 0;
                counterSpecial++;
                prevSizeOfWavePos = sizeOfWavePos;
                prevSizeOfWaveNeg = sizeOfWaveNeg;
                if (sizeOfWavePos < 4 || sizeOfWaveNeg < 4)
                {
                    sizeOfWavePos = 0;
                    sizeOfWaveNeg = 0;
                    return 3;
                }
                long sum = sizeOfWaveNeg + sizeOfWavePos;

                sizeOfWavePos = 0;
                sizeOfWaveNeg = 0;

                if (sum > 34)
                {
                    UnityEngine.Debug.Log("slow!");
                    if (power > 200)
                    {
                        // right
                        return 2;
                    }
                    else if (power < -450)
                    {
                        // left
                        return 0;
                    }
                    else if (power < 0 && power > -350)
                    {
                        // center
                        return 1;
                    }
                }
                else
                {
                    // fast pace
                    return 4;
                }
            }


        }
        return 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        runcheck();       
    }

    void random_animations()
    {
        int m = UnityEngine.Random.Range(0, 4);
        if (m == 0)
        {
            anim.Play("d1", -1, 1f);
        }
        else if (m == 1)
        {
            anim.Play("d2", -1, 1f);
        }
        else if (m == 2)
        {
            anim.Play("d3", -1, 1f);
        }
        else
        {
            anim.Play("d4", -1, 1f);
        }
    }

    void RUN()
    {
        float forward = speed;
        if (firstTime)
        {
            prevForward = forward;
            firstTime = false;
        }
        float speedForward = forward;
        long valueMove = 200;
        long readFromPortInt = readArduino();

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("j", true);
        }

        if (readFromPortInt == 1) // if the delta between the 2 legs is small
        {
            if (playerPosition == 0) // if the player is on the left side
                transform.Translate(MoveDistance, 0, 0);
            else if (playerPosition == 2) // if the player is on the right side
                transform.Translate(-MoveDistance, 0, 0);
            // else the player is in the middle

            // move the player to the center
            playerPosition = 1;
            transform.Translate(0, 0, speedForward);
            prevForward = speedForward;

        }
        else if (readFromPortInt == 2)
        {     
            if (playerPosition == 0) // if the player is on the left side
                transform.Translate(2 * MoveDistance, 0, 0);
            else if (playerPosition == 1) // if the player is in the middle
                transform.Translate(MoveDistance, 0, 0);
            // else the player is on the right

            // move the player to the right
            playerPosition = 2;
            transform.Translate(0, 0, speedForward);
            prevForward = speedForward;

        }
        else if (readFromPortInt == 0)
        {
            if (playerPosition == 2) // if the player is on the right side
                transform.Translate(-2 * MoveDistance, 0, 0);
            else if (playerPosition == 1) // if the player is in the middle
                transform.Translate(-MoveDistance, 0, 0);
            // else the player is on the left

            // move the player to the left
            playerPosition = 0;
            transform.Translate(0, 0, speedForward);
            prevForward = speedForward;

        }
        else if (readFromPortInt == 4)
        {
            if (playerPosition == 0) // if the player is on the left side
                transform.Translate(MoveDistance, 0, 0);
            else if (playerPosition == 2) // if the player is on the right side
                transform.Translate(-MoveDistance, 0, 0);
            // else the player is in the middle

            // move the player to the center
            playerPosition = 1;
            transform.Translate(0, 0, forward * 2);
            prevForward = forward * 2;

        }
        else
        {
            transform.Translate(0, 0, prevForward);
        }
    }

    void runcheck()
    {
        if (Input.GetKeyDown("space"))
        {
            runn = true;
            anim.SetBool("run", runn);
            RUN();
        }
        else if (runn == true)
        {
            audiobeforespace.Stop();
            if (check == true)
            {
                audio.Play();
                check = false;
            }
            RUN();
        }
    }
    void end()
    {
        FindObjectOfType<GameManager>().endgame();
    }
    void endjump()
    {
        anim.SetBool("j", false);
        anim.Play("run");
    }
}
