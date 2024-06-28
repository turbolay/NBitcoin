﻿using NBitcoin.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBitcoin
{
	public class Bitcoin : INetworkSet
	{
		private Bitcoin()
		{
		}

		private Network CreateSignet()
		{
			NetworkBuilder builder = new NetworkBuilder();
			builder.SetChainName(SignetName);
			builder.SetNetworkSet(this);
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000,
				MajorityEnforceBlockUpgrade = 750,
				MajorityRejectBlockOutdated = 950,
				MajorityWindow = 1000,
				BIP34Hash = new uint256(),
				PowLimit = new Target(new uint256("00000377ae000000000000000000000000000000000000000000000000000000")),
				PowTargetTimespan = TimeSpan.FromSeconds(14 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(10 * 60),
				PowAllowMinDifficultyBlocks = false,
				PowNoRetargeting = false,
				RuleChangeActivationThreshold = 1916,
				MinerConfirmationWindow = 2016,
				CoinbaseMaturity = 100,
				SupportSegwit = true,
				SupportTaproot = true
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 111 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 196 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, "tb")
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, "tb")
			.SetBech32(Bech32Type.TAPROOT_ADDRESS, "tb")
			.SetMagic(GetSignetMagic())
			.SetPort(38333)
			.SetRPCPort(38332)
			.SetName("signet")
			.AddAlias("bitcoin-signet")
			.AddAlias("btc-signet")
#if !NOSOCKET
			.AddSeeds(new[]
			{
				"178.128.221.177",
				"2a01:7c8:d005:390::5"
			}.Select(o => new Protocol.NetworkAddress(System.Net.IPAddress.Parse(o))))
#endif
			.SetGenesis("0100000000000000000000000000000000000000000000000000000000000000000000003ba3edfd7a7b12b27ac72c3e67768f617fc81bc3888a51323a9fb8aa4b1e5e4a008f4d5fae77031e8ad222030101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff4d04ffff001d0104455468652054696d65732030332f4a616e2f32303039204368616e63656c6c6f72206f6e206272696e6b206f66207365636f6e64206261696c6f757420666f722062616e6b73ffffffff0100f2052a01000000434104678afdb0fe5548271967f1a67130b7105cd6a828e03909a67962e0ea1f61deb649f6bc3f4cef38c4f35504e51ec112de5c384df7ba0b8d578a4c702b6bf11d5fac00000000");
			var network = builder.BuildAndRegister();
#if !NOFILEIO
			var data = Network.GetDefaultDataFolder("bitcoin");
			if (data != null)
			{
				var signetCookie = Path.Combine(data, "signet", ".cookie");
				RPC.RPCClient.RegisterDefaultCookiePath(network, signetCookie);
			}
#endif
			return network;
		}

		private Network CreateTestNet4()
		{
			NetworkBuilder builder = new NetworkBuilder();
			builder.SetChainName(Testnet4Name);
			builder.SetNetworkSet(this);
			builder.SetConsensus(new Consensus()
			{
				SubsidyHalvingInterval = 210000,
				MajorityEnforceBlockUpgrade = 51,
				MajorityRejectBlockOutdated = 75,
				MajorityWindow = 100,
				BIP34Hash = new uint256(),
				PowLimit = new Target(new uint256("00000000ffffffffffffffffffffffffffffffffffffffffffffffffffffffff")),
				PowTargetTimespan = TimeSpan.FromSeconds(14 * 24 * 60 * 60),
				PowTargetSpacing = TimeSpan.FromSeconds(10 * 60),
				PowAllowMinDifficultyBlocks = true,
				PowNoRetargeting = false,
				RuleChangeActivationThreshold = 1512,
				MinerConfirmationWindow = 2016,
				CoinbaseMaturity = 100,
				SupportSegwit = true,
				SupportTaproot = true
			})
			.SetBase58Bytes(Base58Type.PUBKEY_ADDRESS, new byte[] { 111 })
			.SetBase58Bytes(Base58Type.SCRIPT_ADDRESS, new byte[] { 196 })
			.SetBase58Bytes(Base58Type.SECRET_KEY, new byte[] { 239 })
			.SetBase58Bytes(Base58Type.EXT_PUBLIC_KEY, new byte[] { 0x04, 0x35, 0x87, 0xCF })
			.SetBase58Bytes(Base58Type.EXT_SECRET_KEY, new byte[] { 0x04, 0x35, 0x83, 0x94 })
			.SetBech32(Bech32Type.WITNESS_PUBKEY_ADDRESS, "tb")
			.SetBech32(Bech32Type.WITNESS_SCRIPT_ADDRESS, "tb")
			.SetBech32(Bech32Type.TAPROOT_ADDRESS, "tb")
			.SetMagic(0x283F161C)
			.SetPort(48333)
			.SetRPCPort(48332)
			.SetName("testnet4")
			.AddAlias("bitcoin-testnet4")
			.AddAlias("btc-testnet4")
			.AddAlias("test4")
#if !NOSOCKET
			.AddDNSSeeds(new[]
			{
				new DNSSeedData("bitcoin.sprovoost.nl", "seed.testnet4.bitcoin.sprovoost.nl"),
				new DNSSeedData("wiz.biz", "seed.testnet4.wiz.biz")
			})
			.AddSeeds(Network.LoadNetworkAddresses(Network.pnSeed6_test))
#endif
			.SetGenesis("0100000000000000000000000000000000000000000000000000000000000000000000004e7b2b9128fe0291db0693af2ae418b767e657cd407e80cb1434221eaea7a07a046f3566ffff001dbb0c78170101000000010000000000000000000000000000000000000000000000000000000000000000ffffffff5504ffff001d01044c4c30332f4d61792f323032342030303030303030303030303030303030303030303165626435386332343439373062336161396437383362623030313031316662653865613865393865303065ffffffff0100f2052a010000002321000000000000000000000000000000000000000000000000000000000000000000ac00000000");
			var network = builder.BuildAndRegister();
#if !NOFILEIO
			var data = Network.GetDefaultDataFolder("bitcoin");
			if (data != null)
			{
				var testnet4Cookie = Path.Combine(data, "testnet4", ".cookie");
				RPC.RPCClient.RegisterDefaultCookiePath(network, testnet4Cookie);
			}
#endif
			return network;
		}

		private static uint GetSignetMagic()
		{
			var challengeBytes = DataEncoders.Encoders.Hex.DecodeData("512103ad5e0edad18cb1f0fc0d28a3d4f1f3e445640337489abb10404f2d1e086be430210359ef5021964fe22d6f8e05b2463c9540ce96883fe3b278760f048f5189f2e6c452ae");
			var challenge = new Script(challengeBytes);
			MemoryStream ms = new MemoryStream();
			BitcoinStream bitcoinStream = new BitcoinStream(ms, true);
			bitcoinStream.ReadWrite(challenge);
			var h = Hashes.DoubleSHA256RawBytes(ms.ToArray(), 0, (int)ms.Length);
			return Utils.ToUInt32(h, true);
		}

		public static Bitcoin Instance { get; } = new Bitcoin();

		public Network Mainnet => Network.Main;

		public Network Testnet => Network.TestNet;

		public Network Regtest => Network.RegTest;

		public string CryptoCode => "BTC";

		static readonly ChainName SignetName = new ChainName("Signet");

		static readonly ChainName Testnet4Name = new ChainName("Testnet4");

		Network _Signet;
		public Network Signet
		{
			get
			{
				return _Signet ??= Network.GetNetwork("signet");
			}
		}


		Network _TestNet4;
		public Network TestNet4
		{
			get
			{
				return _TestNet4 ??= Network.GetNetwork("testnet4");
			}
		}

		public Network GetNetwork(ChainName chainName)
		{
			if (chainName == null)
				throw new ArgumentNullException(nameof(chainName));
			if (chainName == ChainName.Mainnet)
				return Mainnet;
			if (chainName == ChainName.Testnet)
				return Testnet;
			if (chainName == Testnet4Name)
				return TestNet4;
			if (chainName == ChainName.Regtest)
				return Regtest;
			if (chainName == SignetName)
				return Signet;
			return null;
		}

		internal Network InitSignet()
		{
			return _Signet = CreateSignet();
		}

		internal Network InitTestNet4()
		{
			return _TestNet4 = CreateTestNet4();
		}
	}
}
