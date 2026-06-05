using MerkleCloudIntegrity.Services;

const int SignatureLeafIndex = 2;
const int SignatureLeafCount = 1024;
const string OutputFolderName = "merkle-cloud-integrity-signature";
const string OutputFileName = "signature-simulation-results.csv";

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

var outputDirectory = Path.Combine(Path.GetTempPath(), OutputFolderName);
var csvPath = Path.Combine(outputDirectory, OutputFileName);
signatureSimulationService.RunSignatureSimulation(
    csvPath,
    SignatureLeafIndex,
    SignatureLeafCount);

Console.WriteLine("Signature simulation completed");
Console.WriteLine($"CSV path: {csvPath}");
