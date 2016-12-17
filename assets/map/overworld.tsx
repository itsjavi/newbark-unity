<?xml version="1.0" encoding="UTF-8"?>
<tileset name="overworld" tilewidth="32" tileheight="32" tilecount="72" columns="9">
 <image source="../img/map/overworld_tiles.png" width="288" height="256"/>
 <tile id="0">
  <objectgroup draworder="index" name="treeTop">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="1">
  <objectgroup draworder="index" name="smallTree">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="2">
  <objectgroup draworder="index" name="berryTree">
   <object id="3" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="6">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="7">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="8">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="9">
  <objectgroup draworder="index" name="treeBottom">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="10">
  <objectgroup draworder="index" name="smallTree2">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="11">
  <properties>
   <property name="Type" value="grass"/>
  </properties>
  <objectgroup draworder="index" name="grass"/>
 </tile>
 <tile id="15">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="16">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="17">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="18">
  <objectgroup draworder="index" name="flowersAni"/>
  <animation>
   <frame tileid="18" duration="500"/>
   <frame tileid="19" duration="500"/>
  </animation>
 </tile>
 <tile id="20">
  <properties>
   <property name="Type" value="grass"/>
  </properties>
  <objectgroup draworder="index" name="grassAni"/>
  <animation>
   <frame tileid="20" duration="100"/>
   <frame tileid="11" duration="100"/>
  </animation>
 </tile>
 <tile id="21">
  <properties>
   <property name="Type" value="water"/>
  </properties>
  <animation>
   <frame tileid="21" duration="500"/>
   <frame tileid="22" duration="500"/>
   <frame tileid="23" duration="500"/>
   <frame tileid="22" duration="500"/>
  </animation>
 </tile>
 <tile id="22">
  <properties>
   <property name="Type" value="water"/>
  </properties>
 </tile>
 <tile id="23">
  <properties>
   <property name="Type" value="water"/>
  </properties>
 </tile>
 <tile id="24">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="25">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="26">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="27">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="28">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="29">
  <objectgroup draworder="index">
   <object id="2" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="30">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
  <animation>
   <frame tileid="30" duration="500"/>
   <frame tileid="31" duration="500"/>
   <frame tileid="32" duration="500"/>
   <frame tileid="31" duration="500"/>
  </animation>
 </tile>
 <tile id="31">
  <objectgroup draworder="index">
   <object id="2" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="32">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="33">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="34">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="35">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="36">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="37">
  <animation>
   <frame tileid="37" duration="500"/>
   <frame tileid="64" duration="500"/>
  </animation>
 </tile>
 <tile id="38">
  <objectgroup draworder="index">
   <object id="2" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="39">
  <properties>
   <property name="Type" value="grass"/>
  </properties>
 </tile>
 <tile id="40">
  <properties>
   <property name="Type" value="grass"/>
  </properties>
  <animation>
   <frame tileid="40" duration="250"/>
   <frame tileid="41" duration="250"/>
  </animation>
 </tile>
 <tile id="41">
  <properties>
   <property name="Type" value="grass"/>
  </properties>
 </tile>
 <tile id="42">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="43">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="44">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="45">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="46">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="47">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="48">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="50">
  <properties>
   <property name="Type" value="text"/>
  </properties>
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="51">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="53">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="54">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="55">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="56">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="58">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="59">
  <properties>
   <property name="Type" value="warp"/>
  </properties>
  <objectgroup draworder="index" name="door">
   <object id="1" x="0" y="0" width="32" height="16"/>
  </objectgroup>
 </tile>
 <tile id="60">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="62">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="63">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="65">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="66">
  <objectgroup draworder="index" name="collisionZone">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="67">
  <objectgroup draworder="index" name="jumpZone"/>
 </tile>
 <tile id="68">
  <objectgroup draworder="index" name="warpZone"/>
 </tile>
 <tile id="69">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="70">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <tile id="71">
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
</tileset>
