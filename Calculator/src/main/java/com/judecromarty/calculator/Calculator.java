/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 */
package com.judecromarty.calculator;

import java.util.Scanner;

/**
 *
 * @author judec
 */
public class Calculator {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        //inputs
        System.out.println("Please enter your first number: ");
        float Input_1 = scanner.nextFloat();
        System.out.println("Please enter your second number: ");
        float Input_2 = scanner.nextFloat();
        scanner.nextLine();//use empty char
        System.out.println("Please enter your symbol: +, -, *, / ");
        String Input_3 = scanner.nextLine();

        if (null == Input_3) {
            System.out.println("This is not a valid calculation");
        } else //calculate
        {
            switch (Input_3) {
                case "+" ->
                    System.out.println("Your number is: " + (Input_1 + Input_2));
                case "-" ->
                    System.out.println("Your number is: " + (Input_1 - Input_2));
                case "*" ->
                    System.out.println("Your number is: " + (Input_1 * Input_2));
                case "/" ->
                    System.out.println("your number is: " + (Input_1 / Input_2));
                default ->
                    System.out.println("This is not a valid calculation");
            }
        }

    }
}
