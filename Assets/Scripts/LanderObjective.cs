﻿/*
 * Copyright (c) 2017 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections;
using UnityEngine;

public class LanderObjective : MonoBehaviour
{
    private bool canRetractLandingPad;

    private bool contactMade;

    private Lander playerLander;

    void Start()
    {
        playerLander = GameObject.Find("Lander").GetComponent<Lander>();

        if (playerLander == null)
        {
            Debug.LogError("Cannot find Lander gameobject. Make sure your Lander is named Lander.");
        }
    }

    public void ActivateLandingPad()
    {
        Debug.Log("Activated landing pad");
        if (canRetractLandingPad == false && contactMade == false)
        {
            StartCoroutine(RectractLandingPad());
        }
    }

    private IEnumerator RectractLandingPad()
    {
        canRetractLandingPad = true;
        contactMade = true;

        yield return new WaitForSeconds(0.5f);

        canRetractLandingPad = false;
        playerLander.allowThrust = false;

        GameObject.Find("Lander").GetComponent<Lander>().EnableRestartButton();
    }

    // Update is called once per frame
    void Update()
    {
        if (canRetractLandingPad)
        {
            var landingPadPositionY = transform.position.y - Time.deltaTime / 3;
            transform.position = new Vector2(transform.position.x, landingPadPositionY);
        }
    }
}