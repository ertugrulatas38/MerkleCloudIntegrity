using MerkleCloudIntegrity.Services;

/// <summary>
/// Application entry point for the MerkleCloudIntegrity console application.
/// </summary>
Console.WriteLine("Merkle Cloud Integrity");
Console.WriteLine("----------------------");

var hashService = new HashService();
var merkleTreeService = new MerkleTreeService(hashService);
var verificationService = new VerificationService(hashService);
var traversalService = new TraversalService(merkleTreeService);
var signatureSimulationService = new SignatureSimulationService(
    merkleTreeService,
    verificationService,
    traversalService);

var outputDirectory = Path.Combine(Path.GetTempPath(), "merkle-cloud-integrity-signature");
var csvPath = Path.Combine(outputDirectory, "signature-simulation-results.csv");
signatureSimulationService.RunSignatureSimulation(csvPath);

Console.WriteLine("Signature simulation completed");
Console.WriteLine($"CSV path: {csvPath}");
