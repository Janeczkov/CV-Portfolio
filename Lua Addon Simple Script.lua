---------------------------------
------- Zao Muggy Plains --------
----------](HARDCORE)[-----------
--- By Xiaospike & Joshwa534 ----
----------- Enjoy!!! ------------
---------------------------------

--[[
100% AFK Zao Muggy Plains for Paladins Youtube:
http://www.youtube.com/watch?v=he_a_uSfWM0
]]

--------- DP SETUP ----------
-- [DP 1] -- Cascade this.
-- [DP 2] -- Cascade this.
-- [DP 3] -- Cascade this.
-- [DP 4] -- Cascade this.
-- [DP 5] -- Cascade this.

--------- BP SETUP ----------
-- [BP 1] -- Main
-- [BP 2] -- Ammo
-- [BP 3] -- Products
-- [BP 4] -- Rares
-- [BP 5] -- Gold

------ REFILL SETTINGS ------
local MinMana = 40 	--- How many mana potions until you leave the hunt?
local destcap = 1800 	--- How many cap you begin the hunt with?
local MaxMana = 250
local maxwall = 50
local maxava = 350

local Hpotion = 23374
local GMpotion = 238
local Mpotion = 237
local Hprice = 310
local Mprice = 50
local ewall = 3166
local ava = 3161

local MinHealth = 10 	--- How many health potions until you leave the hunt?
local MaxHealth = 100 	--- How many health potions you begin the hunt with?

local BPCount = 7 -- main, stackable, niestackable, life ring, poty
local GoldBP = "Orange Backpack" --- Item ID of your gold backpack.
local PotionBP = "Jewelled Backpack"
local amuletbp = "Red Backpack"
local BPmain = Self.Backpack().id
local ringbp = "Brocade Backpack"
local boltbp = "Glooth Backpack"
local arrowbp = "Expedition Backpack"
local MinCap = 1 		--- If less then script will exit spawn.
local HideEquipment = false --- Do you want to minimize your equipment?

local hardcore = true
local Softboots = true --- Do you use softboots?
local butyid = 16112
local userings = true
local ringid = 25698 --butterfly
local ringid = 23529 --plasma

local LogoutStamina = true --- Do you want to logout if low stamina?
local MinStamina = 1400 --- How much stamina do you want to log out at? (60 = 1 hour)
------------- ONLY PICK ONE -------------
local Skill = 'distance' --- What skill do you want to train? (sword, club, axe, distance, magic)

local summon = true
local ezoduslowhunt = false
local Ghastlies = false
local BEASTMODE = true --- How Beast Are You? (Level 200+ if full spawn)
local useamulet = true
local werewolf = 22134 --werewolf
local amulet = 23542 --blue plasma

local useammo = true
local boltid = 25758
local arrowid = 25757
local usebolt = true
local usearrow = true
local maxbolt = 2200
local minbolt = 80
local maxarrow = 350


--------------------------------------------------------------------
--------------------------------------------------------------------
--------------------------------------------------------------------
--------------------------------------------------------------------
--------------------------------------------------------------------

local dwarven = false
local onrefil = false

local MonsterList = {"Undead Dragon", "Demon"}

registerEventListener(WALKER_SELECTLABEL, "onWalkerSelectLabel")

function resetbp()
	Self.CloseContainers()
	Self.UseItem(23721)
	Self.OpenMainBackpack()
	mainbp = Container.New(Self.Backpack().id)
	if userings then
		mainbp:OpenChildren({ringbp, true})
	end
	if useamulet then
		mainbp:OpenChildren({amuletbp, true})
	end
	mainbp:OpenChildren({boltbp, true}, {arrowbp, true}, {PotionBP, true})
	--Container.GetByName(boltbp):OpenChildren({boltbp, true})
	--Container.GetByName(arrowbp):OpenChildren({arrowbp, true})
	boltbpdepth = 0
end

while (#Container.GetAll() < BPCount) do
	Self.CloseContainers()
	Self.UseItem(23721)
	Self.OpenMainBackpack()
	mainbp = Container.New(Self.Backpack().id)
	if userings then
		mainbp:OpenChildren({ringbp, true})
	end
	if useamulet then
		mainbp:OpenChildren({amuletbp, true})
	end
	mainbp:OpenChildren({boltbp, true}, {arrowbp, true}, {PotionBP, true})
	--Container.GetByName(boltbp):OpenChildren({boltbp, true})
	--Container.GetByName(arrowbp):OpenChildren({arrowbp, true})
end
mainbp = Container.New(Self.Backpack().id)
lootbp = Container.New(0)

Targeting.Start()
Looter.Start()

print([[
~ JECHANKA ~]])

if Softboots then
	Module.New("Soft", function(Soft)
		local MonsterCount = 2 -- How many monsters until you equip the special ring?
		local MonsterRange = 2 
		Self.ManaPercent = function ()
    		return math.abs(Self.Mana() / (Self.MaxMana() * 0.01))
		end
		local mob = Self.GetTargets(MonsterRange)
	   	local mobCount = 0
	    for i = 1, #mob do
			if table.contains(MonsterList, mob[i]:Name()) then
				mobCount = mobCount + 1
		    end
	   	end
		if (Self.ManaPercent() <= 85) and (Self.Feet().id ~= 3549) and (Self.ItemCount(6529) > 0) and (Self.isInPz() == false) and (mobCount < MonsterCount) then
			Self.Equip(6529, "feet")
			Soft:Delay(500)
		elseif ((Self.Feet().id == 6530) or Self.isInPz() or (mobCount >= MonsterCount) or dwarven or (Self.ManaPercent() > 95)) and (Self.Feet().id ~= butyid)  then
			Self.Equip(butyid, "feet")
			Soft:Delay(500)
		end
		Soft:Delay(1000)
	end)
end

--[[Module.New("emergency", function(emergency)
	if (Self.ItemCount(Mpotion) <= (MinMana / 3 * 2)) then
		Targeting.Stop()
	end
	emergency:Delay(5000)
end)
summoncast = false
]]--

Module.New("emergency", function(emergency)
	if (Self.ItemCount(Mpotion) <= (MinMana / 2)) or (Self.ItemCount(boltid) <= (minbolt / 2)) then
		Targeting.StartIgnoring()
		Looter.Stop()
		local gorefil = true
		if not onrefil and onhunt then
			gotoLabel("refil")
		end
	end
	emergency:Delay(3000)
end)



if (summon) then
	hudcount = HUD.New(25, 300, "Count: Not available", 95, 247, 247)
	hudtime = HUD.New(25, 330, "Time since cast: Not available", 95, 247, 247)
	local count = 0 
	local lastcast = 0
	local timepassed = 0
	local summoned = true
	Module.New("summ", function(summ)
		if (Self.Level() >= 200) and (summoncast) and (Self.ItemCount(Mpotion) > (MinMana*2)) then
			if (timepassed == 0) or (timepassed > (60 * 30)) then 
				print("modul")
				--LocalSpeechProxy.OnReceive("prox", function(_, msgType, speaker, level, text)
				ErrorMessageProxy.OnReceive("prox", function(_, text)
					if (text == "You do not have enough mana.") then
						while (Self.Mana() < 2000) and (Self.Health() > 1200) do
							Self.UseItemWithMe(Mpotion)
							summ:Delay(300)
						end
						Self.Say("utevo gran res sac")
					end
				end)
				print("cast")
				Self.Say("utevo gran res sac")
				local specs = Self.GetSpectators(false)
				for k, target in ipairs(specs) do
					if (target:isSelfSummon()) then --(target:Name() == "Emberwing") and 
						if not summoned then
							summoned = true
							count = count + 1
							lastcast = os.time()
							break
						end
					end
					summoned = false
				end
			end
		end
		if (count > 0) then
			timepassed = os.time() - lastcast
			hudcount:SetText("Count: " .. count)
			hudtime:SetText("Time since cast: " .. (timepassed / 60))
		end
		summ:Delay(3000)
	end)
end


if useamulet then
	Module.New("Werewolf amulet", function(Modamulet)
		if not onhunt and (Self.ItemCount(22060) > 0) and (Self.ItemCount(22083) > 0) and (Self.isInPz() == false) then
			Self.Dequip("amulet", mainbp)
			local moonlightspot = 0
			for spot = 0, mainbp:ItemCount() do
				local spotData = mainbp:GetItemData(spot)
				if (spotData.id == 22083) then
					moonlightspot = spot
					break
				end
			end
			for spot = 0, mainbp:ItemCount() do
				local spotData = mainbp:GetItemData(spot)
				if (spotData.id == 22060) then
					mainbp:UseItemWithContainerItem(moonlightspot, mainbp, spot)
					Self.Equip(22134, "amulet")
					break
				end
			end
			Modamulet:Delay(500)
		elseif Self.isInPz() or not onhunt then
			Self.Dequip("amulet", mainbp)
			Modamulet:Delay(500)
		elseif onhunt and (Self.ItemCount(amulet) > 0) then
			Self.Equip(amulet, "amulet")
		end
		Modamulet:Delay(3000)
	end)
end


Module.New("Curse", function(Curse)
	while (Self.isCursed()) and (Self.Health() > 1400) do
		Self.Cast("exana mort", 40)
		Curse:Delay(80)
	end
	Curse:Delay(3000)
end)

Module.New("retreat", function(retreat)
		local MonsterCount = 2 -- How many monsters until you equip the special ring?
		local MonsterRange = 5

		local mob = Self.GetTargets(MonsterRange)
	   	local mobCount = 0
	    for i = 1, #mob do
			if table.contains(MonsterList, mob[i]:Name()) then
				mobCount = mobCount + 1
		    end
	   	end
	   	if (mobCount >= MonsterCount) and onhunt then
			Targeting.StartIgnoring()
			gotoLabel("retreat")
		end

	if (Self.Position().y >= 32289) and (Self.Position().y <= 32291) and (Self.Position().z == 15) and (Self.TargetID() ~= 0) then
		Targeting.StartIgnoring()
		gotoLabel("retreat")
	end
	retreat:Delay(500)
end)

spots = {
		{x = 33319, y = 32331, z = 13}
			}

Module.New("walle", function(walle)
	local MonsterCount = 1 -- How many monsters until you equip the special ring?
	local MonsterRange = 5
	local mob = Self.GetTargets(MonsterRange)
	local mobCount = 0
	for i = 1, #mob do
		if table.contains(MonsterList, mob[i]:Name()) then
			mobCount = mobCount + 1
		end
	end
	if (mobCount >= MonsterCount) then
		for i, curspot in ipairs(spots) do
			local topitem = Map.GetTopUseItem(curspot.x, curspot.y, curspot.z).id
			if (topitem ~= 2122) and (topitem ~= 0) and (Self.ItemCount(3166) > 0) then
				--print(Map.GetTopUseItem(curspot.x, curspot.y, curspot.z).id)
				Self.UseItemWithGround(3166, curspot.x, curspot.y, curspot.z)
	walle:Delay(1000)
			end
		end
	end
	walle:Delay(500)
end)

boltbpcontdepth = 0
arrowbpcontdepth = 0
local boltbpcont = Container.GetByName(boltbp)
--local mainbp = Container.GetByName(mainbp)
local arrowbpcont = Container.GetByName(arrowbp)
local ringbpcont = Container.GetByName(ringbp)
local amuletbpcont = Container.GetByName(amuletbp)
local PotionBPcont = Container.GetByName(PotionBP)
local boltbpindex = boltbpcont:Index()
local arrowbpindex = arrowbpcont:Index()
Module.New("MoveManas", function(Mod)
	if (Self.ItemCount(Mpotion) >= 1) or (Self.ItemCount(Hpotion) >=1) or (Self.ItemCount(ava) >=1) or (Self.ItemCount(boltid) >=1) or (Self.ItemCount(arrowid) >=1) then
		local MainBp = mainbp
		conts = Container.iContainers()
		for i, cont in conts do
			for spot = 0, cont:ItemCount() do
				local item = cont:GetItemData(spot)
				if ((item.id == GMpotion) or (item.id == Mpotion) or (item.id == Hpotion) or (item.id == ava)) and ( (cont:Name() == amuletbpcont:Name()) or (cont:Name() == ringbpcont:Name()) or (cont:Name() == boltbpcont:Name()) or (cont:Name() == mainbp:Name()) or (cont:Name() == lootbp:Name())) and not (PotionBPcont:isFull()) then
					print(cont:Name())
					cont:MoveItemToContainer(spot, PotionBPcont:Index(), 0)
					Mod:Delay(500)     
					break
				elseif (((Self.ItemCount(boltid, mainbp:Index()) > 200) and (cont:Name() == mainbp:Name())) or ((cont:Name() == ringbpcont:Name()) or (cont:Name() == amuletbpcont:Name()) or (cont:Name() == arrowbpcont:Name()) or (cont:Name() == PotionBPcont:Name()))) and (item.id == boltid) then
					print("heck2")
					if not (boltbpcont:isFull()) then
						cont:MoveItemToContainer(spot, boltbpcont:Index(), 19)
						print(Item.GetName(item.id))
						Mod:Delay(500)
						break
					else
						if (boltbpcont:GetItemData(19).id == Item.GetID(boltbpcont:Name())) then
							boltbpcont:UseItem(19, true)
							boltbpcontdepth = boltbpcontdepth + 1
							firstboltbp = false
							Mod:Delay(500)
							break
						end
					end
				elseif (((Self.ItemCount(arrowid, mainbp:Index()) > 200) and (cont:Name() == mainbp:Name())) or ((cont:Name() == ringbpcont:Name()) or (cont:Name() == amuletbpcont:Name()) or (cont:Name() == boltbpcont:Name()))) and ((item.id == arrowid) or (item.id == Mpotion) or (item.id == ava)) then
					print("heck3")
					print(Self.ItemCount(arrowid, mainbp:Index()))
					if not (arrowbpcont:isFull()) then
						cont:MoveItemToContainer(spot, arrowbpcont:Index(), 19)
						print(item.id)
						Mod:Delay(500)
						break
					else
						if (arrowbpcont:GetItemData(19).id == Item.GetID(arrowbpcont:Name())) then
							arrowbpcont:UseItem(19, true)
							arrowbpcontdepth = arrowbpcontdepth + 1
							firstarrowbp = false
							Mod:Delay(500)
						end
					end
				end
			end
		end
		if (Self.ItemCount(boltid, mainbp:Index()) < 1) and (Self.ItemCount(boltid, boltbpcont:Index()) > 0) then
			for spot = 0, boltbpcont:ItemCount() do
				local item = boltbpcont:GetItemData(spot)
				if (item.id == boltid) then
					boltbpcont:MoveItemToContainer(spot, mainbp:Index(), 0)
					Mod:Delay(500)
					break
				end
			end
		end
		if (Self.ItemCount(arrowid, mainbp:Index()) < 1) and (Self.ItemCount(arrowid, arrowbpcont:Index()) > 0) then
			for spot = 0, arrowbpcont:ItemCount() do
				local item = arrowbpcont:GetItemData(spot)
				if (item.id == arrowid) then
					arrowbpcont:MoveItemToContainer(spot, mainbp:Index(), 0)
					Mod:Delay(500)
					break
				end
			end
		end
		if ((Self.ItemCount(arrowid) - Self.Ammo().count) == 0) and not firstarrowbp then
			arrowbpcont:GoBack()
			wait(500)
			local conttocheck = Container.New(arrowbpindex)
			print(conttocheck:Name())
			if (conttocheck:Name() == mainbp:Name()) then
				firstarrowbp = true
				for spott = 0, conttocheck:ItemCount() do
					local itemm = conttocheck:GetItemData(spott)
					if (Item.GetName(itemm.id) == string.lower(arrowbp)) then
						conttocheck:UseItem(spott, true)
						Mod:Delay(500)
						break
					end
				end
			end
		end
		if ((Self.ItemCount(boltid) - Self.Ammo().count) == 0) and not firstboltbp then
			boltbpcont:GoBack()
			wait(500)
			local conttocheck = Container.New(boltbpindex)
			print(conttocheck:Name())
			if (conttocheck:Name() == mainbp:Name()) then
				firstboltbp = true
				for spott = 0, conttocheck:ItemCount() do
					local itemm = conttocheck:GetItemData(spott)
					if (Item.GetName(itemm.id) == string.lower(boltbp)) then
						conttocheck:UseItem(spott, true)
						Mod:Delay(500)
						break
					end
				end
			end
			--Mod:Delay(500)
		end
	end
	Mod:Delay(200)
end)


if useammo then
	local MonsterCount = 3 -- How many monsters until you equip the special ring?
	local MonsterRange = 5
	Module.New("Ammo", function(Ammo)
		local mob = Self.GetTargets(MonsterRange)
		local mobCount = 0
		players = 0
		for i = 1, #mob do
			if table.contains(MonsterList, mob[i]:Name()) then
				mobCount = mobCount + 1
			end
		end
		for name, obj in Creature.iPlayers(7) do
			players = players + 1
		end
		local mincount = 101
		local minspot = 0
		
		if not Targeting.IsIgnoring() and Walker.IsEnabled() then
			if ((mobCount >= MonsterCount) or (Self.ItemCount(boltid) == 0)) and ((Self.ItemCount(22866) > 0) and (Self.ItemCount(arrowid) > 0)) and (players == 0) then
				if ((Self.Ammo().id == arrowid) and ((Self.ItemCount(arrowid) - Self.Ammo().count) > 0)) or (Self.ItemCount(arrowid) > 0) then
					
					while (Self.Weapon().id ~= 22866) do
						Self.Equip(22866, "weapon")
					Ammo:Delay(300)
					end

					while (Self.Ammo().id ~= arrowid) or (Self.Ammo().count < 80) do
						--Self.Dequip("ammo", mainbp)
						local mincount = 101
						local minspot = 0
						local finalcont = ""
						--for i, cont in Container.iContainers() do
							for spot = 0, mainbp:ItemCount() do
								local spotData = mainbp:GetItemData(spot)
								if (spotData.id == arrowid) and (spotData.count < mincount) then
									minspot = spot
									mincount = spotData.count
									--finalcont = cont
								end
							end
						--end
							--print(finalcont:Name())
							print(minspot)
							print(mincount)
							if (mincount ~= 101) then
								mainbp:MoveItemToEquipment(minspot, "ammo")
							else
								break
							end
							Ammo:Delay(300)
					end
				end

			elseif ((mobCount < MonsterCount) or (Self.ItemCount(arrowid) == 0)) and ((Self.ItemCount(22867) > 0) and (Self.ItemCount(boltid) > 0)) or (players > 0) then
				if ((Self.Ammo().id == boltid) and ((Self.ItemCount(boltid) - Self.Ammo().count) > 0)) or (Self.ItemCount(boltid) > 0) then
					while (Self.Weapon().id ~= 22867) do
						Self.Equip(22867, "weapon")
						Ammo:Delay(300)
					end
					while (Self.Ammo().id ~= boltid) or (Self.Ammo().count < 80) do
						--Self.Dequip("ammo", mainbp)
						local mincount = 101
						local minspot = 0
						local finalcont = ""
						--for i, cont in Container.iContainers() do
						for spot = 0, mainbp:ItemCount() do
							local spotData = mainbp:GetItemData(spot)
							if (spotData.id == boltid) and (spotData.count < mincount) then
								minspot = spot
								mincount = spotData.count
								finalcont = cont
							end
						end
							--Ammo:Delay(3000)
						--end
						print(minspot)
						print(mincount)
						--print(finalcont:Name())
						if (mincount ~= 101) then
							mainbp:MoveItemToEquipment(minspot, "ammo")
						else
							break
						end
						Ammo:Delay(300)
					end
				end
			end
		end
		Ammo:Delay(100)
	end)
end


if userings then
	Module.New("Sword ring", function(Modring)
		--if (Self.Ring().id == 0) and (Self.ItemCount(ringid) > 0) and not dwarven then
		--	Self.Equip(ringid, "ring")
		--	wait(500)
		if (Self.Ring().id == 0) and (Self.ItemCount(ringid) > 0) and onhunt and not dwarven then
			Self.Equip(ringid, "ring")
			Modring:Delay(300)
		elseif not dwarven and (Self.Ring().id == 3099) then
			Self.Dequip("ring", mainbp)
			Modring:Delay(300)
		end
		Modring:Delay(3000)
	end)
end
	
function onWalkerSelectLabel(labelName)
	if (labelName == "check1") or (labelName == "check2") then
		if (Self.ItemCount(Mpotion) <= MinMana) or (Self.ItemCount(boltid) <= minbolt) or (Self.ItemCount(Hpotion) <= MinHealth) or (Self.Cap() < MinCap) or ((LogoutStamina) and (Self.Stamina() < 840)) then
			gotoLabel("refil")
		else
			summoncast = true
			gotoLabel("hunt")
		end
		
	elseif (labelName == "softrepair")then
		if (Softboots == true) and (Self.ItemCount(6530) > 0) then
			gotoLabel("gosoft")
		else
			gotoLabel("nosoft")
		end	

	elseif (labelName == "after retreat") then
		Targeting.Stop()
		Targeting.Start()
		Targeting.StopIgnoring()
		Walker.Stop()
		Walker.Start()

	elseif (labelName == "goskos") then
		local p1 = Map.GetTopMoveItem(33328, 32326, 13)
		local mw1 = Map.GetTopUseItem(33328, 32326, 13)
		local p2 = Map.GetTopMoveItem(33327, 32327, 13)
		local mw2 = Map.GetTopUseItem(33327, 32327, 13)

		if ((p1.id == 99) or (p2.id== 99) or (mw1.id == 2129) or (mw2.id == 2129)) then
			gotoLabel("retreatfull")
		end


	elseif (labelName == "hunt") then
		dwarven = true
		onrefil = false
		onhunt = true
		Targeting.StopIgnoring()
		Looter.Start()

	elseif (labelName == "refil") or (labelName == "beforemainroom") then
		summoncast = false
		dwarven = true
		firstarrowbp = false
		firstboltbp = false
		while (Self.ItemCount(3097) > 0) and (Self.Ring().id ~= 3099) do
			Self.Equip(3097, "ring")
			wait(300)
		end
		Targeting.StartIgnoring()

	elseif (labelName == "aftermainroom")then
		dwarven = false
		onrefil = true
		onhunt = false

	elseif (labelName == "checkpz") then
		--[[if (Self.PvpIcon() == todo) then
			Walker.Delay(30000)
			gotoLabel("losepz")
		end]]--


		
	elseif (labelName == "bank") then
		Walker.Stop()
		Self.SayToNpc({"hi", "deposit all", "yes"}, 65)
		wait(500,600)
		Self.SayToNpc({"balance"}, 65)
		wait(500,600)
		Walker.Start()

	elseif (labelName == "withdrawitems") then
		Self.WithdrawItems(15, {amulet, amuletbp, (amuletbpcont:EmptySlots() - 1) })
		Self.WithdrawItems(14, {ringid, ringbp, (ringbpcont:EmptySlots() - 1 ) })
		Self.WithdrawItems(13, {"Brown Mushroom", amuletbp, (99 - Self.ItemCount("Brown Mushroom")) })

		if (Self.Cap() > destcap) then
			firstboltbp = false
			Self.WithdrawItems(10, {arrowid, arrowbp, (maxarrow - Self.ItemCount(arrowid)) })
			local boltstotake = math.floor((Self.Cap() - math.max(((MaxMana - Self.ItemCount(Mpotion)) * 2.9), 0) - destcap) / 0.9)
			print(boltstotake)
			--Self.WithdrawItems(6, {boltid, boltbp, (maxbolt - Self.ItemCount(boltid)) })
			Self.WithdrawItems(6, {boltid, boltbp, boltstotake})
		end
	elseif (labelName == "deposit") then
		Walker.Stop()
		Self.ReachDepot()
		Self.DepositItems({7642, 7},{5876, 7}, {5881, 7}, {10408, 7}, {10409, 7}, {10410, 7}, {3028, 7}, {11673, 7}, {10449, 7}, {10310, 7}, {3032, 7}, {10450, 7}, {5944, 7}, {6499, 7}, {10418, 7}, {10416, 7}, {10408, 7}, {10410, 7}, {3033, 7})
		Self.DepositItems({10392, 8}, {10438, 8}, {10451, 8}, {812			, 8}, {813, 8}, {822, 8}, {3428, 8}, {10385, 8}, {10323, 8},{10390, 8}, {10387, 8}, {10384, 8}, {10388, 8}, {10386, 8}, {8043, 8}, {10439, 8}, {3071, 8}, {3037, 8})
		wait(1500,1900)

	elseif (labelName == "CheckSell") then
		if (SellStuff == true) then
			gotoLabel("Selldoor")
		else
			gotoLabel("NoSell")
		end

	elseif (labelName == "SellItems") then
		if (SellItems) then
			Walker.Stop()
Self.SayToNpc({"Hi", "Trade"}, 65)
			wait(1500)
			Self.ShopSellAllItems(10323)
			wait(500)
			Self.ShopSellAllItems(10390)
			wait(500)
			Self.ShopSellItem(10387, (Self.ShopGetItemSaleCount(10387) - 1))
			wait(500)
			Self.ShopSellItem(10384, (Self.ShopGetItemSaleCount(10384) - 1))
			wait(500)
			Self.ShopSellAllItems(10388)
			wait(500)
			Self.ShopSellItem(10386, (Self.ShopGetItemSaleCount(10386) - 1))
			wait(500)
			Self.ShopSellAllItems(10418)
			wait(500)
			Self.ShopSellAllItems(10323)
			wait(500)
			Self.ShopSellAllItems(10416)
			wait(500)
			Self.ShopSellAllItems(10408)
			wait(500)
			Self.ShopSellAllItems(10392)
			wait(500)
			Self.ShopSellAllItems(10410)
			wait(500)
			Walker.Start()
		end	
		Walker.Start()
		
	elseif (labelName == "potions") then
		Walker.Stop()
		Self.SayToNpc({"hi", "trade"}, 65)
		wait(1000)
		BuyItems(Hpotion, MaxHealth)
		wait(500)
		BuyItems(ewall, maxwall)
		wait(500)
		BuyItems(ava, maxava)
		wait(500)
		local mpuptobuy = math.floor((Self.Cap() - destcap) / 2.9) + Self.ItemCount(Mpotion)
		BuyItems(Mpotion, MaxMana)
		wait(500)
		Walker.Start()

	elseif (labelName == "ammos") then
		Walker.Stop()
		Self.SayToNpc({"hi", "trade"}, 65)
		wait(1000)
		if (boltbpdepth > 0) then
			BuyItems(boltid, (maxbolt - (boltbpdepth * 2000)))
			wait(500)
		else
			BuyItems(boltid, maxbolt)
			wait(500)
		end
		if (arrowbpdepth > 0) then
			BuyItems(arrowid, (maxarrow - (arrowbpdepth * 2000)))
			wait(500)
		else
			BuyItems(arrowid, maxarrow)
			wait(500)
		end
		if (Self.ItemCount(boltid) < minbolt) and (Self.Cap() < 2500) then
			gotoLabel("beforeammo")
		end
		Walker.Start()

	elseif (labelName == "resetbp") then
		Walker.Stop()

		resetbp()
		if (HideEquipment) then
			Client.HideEquipment()
			wait(400,600)
		end
		Walker.Start()
	end
end

----------------------- Functions ----------------------
function SellItems(item) -- item = item ID
	wait(300, 1700)
	Self.ShopSellItem(item, Self.ShopGetItemSaleCount(item))
	wait(900, 1200)
end

function BuyItems(item, count) -- item = item id, count = how many you want to buy up to
	wait(900, 1200)
	if (Self.ItemCount(item) < count) then
	Self.ShopBuyItem(item, (count-Self.ItemCount(item)))
	wait(200, 500)
	end
end

Self.ReachDepot = function (tries)
	local tries = tries or 3
	Walker.Stop()
	local DepotIDs = {3497, 3498, 3499, 3500}
	local DepotPos = {}
	for i = 1, #DepotIDs do
		local dps = Map.GetUseItems(DepotIDs[i])
		for j = 1, #dps do
			table.insert(DepotPos, dps[j])
		end
	end
	local function gotoDepot()
		local pos = Self.Position()
		print("Depots found: " .. tostring(#DepotPos))
		for i = 1, #DepotPos do
			location = DepotPos[i]
			Self.UseItemFromGround(location.x, location.y, location.z)
			wait(1000, 2000)
			if Self.DistanceFromPosition(pos.x, pos.y, pos.z) >= 1 then
				wait(5000, 6000)
				if Self.DistanceFromPosition(location.x, location.y, location.z) == 1 then
					Walker.Start()
					return true
				end
			else
				print("Something is blocking the path. Trying next depot.")
			end
		end
		return false
	end
	
	repeat
		reachedDP = gotoDepot()
		if reachedDP then
			return true
		end
		tries = tries - 1
		sleep(100)
		print("Attempt to reach depot was unsuccessfull. " .. tries .. " tries left.")
	until tries <= 0

	return false
end

Map.GetUseItems = function (id)
    if type(id) == "string" then
        id = Item.GetID(id)
    end
    local pos = Self.Position()
	local store = {}
    for x = -7, 7 do
        for y = -5, 5 do
            if Map.GetTopUseItem(pos.x + x, pos.y + y, pos.z).id == id then
                itemPos = {x = pos.x + x, y = pos.y + y, z = pos.z}
				table.insert(store, itemPos)
            end
        end
    end
    return store
end