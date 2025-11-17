# Naive Bayes Email Classifier

## Overview
This project implements a simple Naive Bayes classifier in C# that categorizes text documents as **spam** or **not spam** based on word frequency statistics learned from a training dataset. The program scans two folders—`spam` and `notspam`—builds word distribution models for each class, and applies Bayes' rule with Laplace smoothing to classify unseen text.

The program demonstrates foundational concepts in probabilistic text classification, including tokenization, bag-of-words modeling, class priors, likelihood estimation, and log-probability computation for numerical stability. All functionality is implemented using only core .NET libraries with no external dependencies.

## Features
- **Two-Class Email Classification:** Differentiates between spam and notspam categories based based on trained word statistics.
- **Bag-of-Words Model:** Counts word occurrences in each category to estimate likelihoods.
- **Laplace Smoothing:** Ensures stable probability estimates and prevents zero-probability issues.
- **Log-Probability Scoring:** Avoids numerical underflow and improves classification stability.
- **Classification Log:** Outputs per-word likelihoods and final log scores for both spam and notspam classes.
- **Directory-Based Training:** Automatically loads all `.txt` files from `spam/` and `notspam/` folders.

## Requirements
- .NET 7.0 or later

## Installation
Clone the repository and restore dependencies:

```bash
git clone https://github.com/your-username/NaiveBayesEmailClassifier.git
cd NaiveBayesEmailClassifier
dotnet restore

## Usage

1. Create two folders in the project root:

/spam
/notspam

2. Add your training emails as `.txt` files inside each folder.

3. (Optional) Edit the test document inside `Program.cs` or modify the program to accept user input.

4. Run the classifier using:
```bash
dotnet run

The program will display:

* Number of spam and notspam training documents

* Vocabulary size

* Per-word likelihoods P(w|spam) and P(w|notspam)

* Log-likelihood scores for each class

* The final predicted label (SPAM or NOT SPAM)

## Notes
- This project was created specifically for CISC 7412X Artificial Intelligence II.

- Training data (spam/notspam text files) is intentionally excluded from version control.