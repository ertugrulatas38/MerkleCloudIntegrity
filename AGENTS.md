# AGENTS.md

# Project Name

MerkleCloudIntegrity

# Project Type

IEEE-oriented academic software project

# Main Goal

Develop a .NET 8 C# application based on Merkle Tree structures for cloud data integrity, traversal optimization, and performance analysis.

The project will support an IEEE-style research paper developed with low similarity and original academic writing principles.

Development will proceed in phases:

1. Console-based algorithm and benchmark core
2. GUI integration (later phase)

---

# Research Scope

The application must support the following research objectives:

1. Merkle Tree verification system
2. Cloud data integrity using Merkle Tree hash model
3. Optimization of traversal algorithms on Merkle Trees
4. Investigation of the trade-off between memory usage and execution time
5. Performance enhancement for large-scale hash-based signature systems

The implementation must allow experimental evaluation and benchmarking.

---

# Literature Foundation

The project is inspired by the following literature.

## Reference 1

Garg, N., Bawa, S.
RITS-MHT: Relative Indexed and Time Stamped Merkle Hash Tree Based Data Auditing Protocol for Cloud Computing.
DOI:
10.1016/j.jnca.2017.02.005

Focus:

* Cloud integrity auditing
* Timestamped Merkle Tree
* Dynamic verification

## Reference 2

HB+-MHT
Lightweight and Efficient Data Integrity Verification Using Merkle Hash Tree.
DOI:
10.1155/2022/9473246

Focus:

* Lightweight verification
* Integrity auditing
* Performance comparison

## Reference 3

Merkle Tree Traversal Revisited
DOI:
10.1007/978-3-540-88403-3_8

Focus:

* Traversal optimization
* Authentication path generation
* Memory-time trade-off

## Reference 4

Fractal Merkle Tree Traversal
DOI:
10.1007/3-540-36563-X_21

Focus:

* Large-scale traversal
* Reduced memory consumption
* Efficient authentication path generation

## Reference 5

GMSS
Merkle Signatures with Virtually Unlimited Signature Capacity
DOI:
10.1007/978-3-540-72738-5_3

Focus:

* Hash-based signatures
* Large-scale Merkle systems
* Signature performance

---

# Technology Stack

Language:
C#

Framework:
.NET 8

Initial Application Type:
Console Application

Future Expansion:
GUI (WPF or WinForms)

Hash Algorithm:
SHA-256

---

# Architecture Rules

Use modular object-oriented design.

Folder structure:

Models/
Services/
Benchmarks/

Suggested classes:

Models

* MerkleNode
* DataBlock
* ProofItem
* BenchmarkResult

Services

* HashService
* MerkleTreeService
* VerificationService
* CloudIntegrityService
* TraversalService
* BenchmarkService
* SignatureSimulationService

Program.cs acts only as application entry point.

Business logic must remain inside Services.

---

# Coding Standards

Use:

* SOLID principles
* XML documentation comments
* Meaningful naming
* Small reusable methods
* Separation of concerns
* Exception-safe code

Avoid:

* Large monolithic methods
* Hardcoded paths
* Duplicate logic
* UI logic mixed with algorithms

Code should be clean and suitable for publication-oriented reproducibility.

---

# Phase Roadmap

## Phase 1

Core Merkle System

Implement:

* SHA256 hashing
* File block splitting
* Merkle Tree construction
* Merkle Root generation
* Merkle Proof generation
* Proof verification

Expected outcome:

Working integrity verification.

---

## Phase 2

Cloud Integrity Model

Implement:

* Dynamic data modification
* Timestamp/version support
* Cloud integrity simulation

Expected outcome:

Cloud auditing experiments.

---

## Phase 3

Traversal Optimization

Implement and compare:

* Classical traversal
* Optimized traversal
* Cached or fractal traversal

Measure:

* Execution time
* Memory usage
* Hash computation count

Expected outcome:

Traversal performance study.

---

## Phase 4

Benchmark Framework

Implement:

* Stopwatch timing
* Memory measurement
* CSV result export
* Experiment logging

Expected outcome:

Data for IEEE tables and figures.

---

## Phase 5

Hash-Based Signature Simulation

Implement:

* GMSS/XMSS-inspired logic
* Authentication path reuse
* Large tree simulation

Expected outcome:

Signature performance analysis.

---

# Experimental Metrics

Benchmark the following:

* Merkle Tree build time
* Verification time
* Memory usage
* Number of hash operations
* Traversal cost
* Scalability with file size
* Root regeneration time after modifications

Use reproducible experiments.

---

# Academic Goal

The software supports an IEEE-format research paper.

Results must be reproducible.

Implementation should provide original experimental findings suitable for academic publication.

Avoid copying external code without attribution.
